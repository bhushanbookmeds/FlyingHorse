using NonProfitCRM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResults> ValidateUser(LoginModel loginModel);

        Task SignIn(Users user, bool isPersistent = false);

        Task SignOut();

        Task<IEnumerable<UserRole>> GetUserRoles(int userId);

        Task<Users> GetAuthenticatedCustomer();
    }
}
