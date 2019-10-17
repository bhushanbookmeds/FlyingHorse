using AutoMapper;
using NonProfitCRM.Data;
using NonProfitCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;

        public UserService(IEncryptionService encryptionService)
        {
            this._unitOfWork = new UnitOfWork();
            this._encryptionService = encryptionService;
        }

        public async Task DeleteUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _unitOfWork.UsersRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Users> GetUserById(int id)
        {
            try
            {
                var user = await _unitOfWork.UsersRepository.GetByIDAsync(id);

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesByUserId(int userId)
        {
            try
            {
                var userRoleMappings = await _unitOfWork.UserRoleMappingRepository.GetManyAsync(ur => ur.UserId == userId);

                var userRoles = userRoleMappings.Select(usr_role => _unitOfWork.UserRoleRepository.GetByID(usr_role.RoleId));

                return userRoles;

            }catch(Exception ex) { return null; }
        }

        public async Task<IEnumerable<Users>> GetUsers(string orgId = null)
        {
            try
            {
                var users = from user in _unitOfWork.UsersRepository.GetDbSet()
                            select user;
                if (!string.IsNullOrWhiteSpace(orgId))
                    users = users.Where(u => u.OrgId.Equals(orgId));           

                return users.ToList();
            }
            catch (Exception) { return null; }
        }

        public async Task<bool> RegisterUser(RegistrationModel registrationModel)
        {
            try
            {
                var user = Mapper.Map<RegistrationModel, Users>(registrationModel);

                user.Password = _encryptionService.EncryptText(user.Password);
                user.UpdatedDate = DateTime.UtcNow;
                user.CreatedDate = DateTime.UtcNow;

                await _unitOfWork.UsersRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();

                var userRoleMapping = new UserRoleMapping
                {
                    RoleId = registrationModel.UserRoleId,
                    UserId = user.Id
                };

                await _unitOfWork.UserRoleMappingRepository.InsertAsync(userRoleMapping);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex) { return false; }
        }

        public async Task UpdateUser(Users user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserRole(int userId,int userRoleId)
        {
            try
            {
                if (userId <= 0 && userRoleId <= 0)
                    throw new ArgumentNullException();

                var userRoleMapping = await _unitOfWork.UserRoleMappingRepository.GetAsync(ur => ur.UserId == userId);

                userRoleMapping.RoleId = userRoleId;
                userRoleMapping.UserId = userId;

                _unitOfWork.UserRoleMappingRepository.Update(userRoleMapping);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }
    }
}
