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

        public async Task<IEnumerable<Users>> GetUsers()
        {
            try
            {
                var users = await _unitOfWork.UsersRepository.GetAllAsync();

                return users;
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

        public async Task UpdateUser(RegistrationModel user)
        {
            throw new NotImplementedException();
        }
    }
}
