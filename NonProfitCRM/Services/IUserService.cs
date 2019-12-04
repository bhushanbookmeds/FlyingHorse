using Core.Domain;
using NonProfitCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public interface IUserService
    {
        Task<Users> GetUserById(int id);

        Task<IEnumerable<Users>> GetUsers(string orgId = null);

        Task<IEnumerable<UserRole>> GetUserRolesByUserId(int userId);

        Task<bool> RegisterUser(RegistrationModel user);

        Task UpdateUser(Users user);

        Task UpdateUserRole(int userId, int userRoleId);

        Task DeleteUser(Users user);
    }
}
