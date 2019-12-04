using Core.Domain;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using NonProfitCRM.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NonProfitCRM.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        private Users _cachedCustomer;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor,
                                     IEncryptionService encryptionService,
                                     IUserService userService,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public virtual Task<IEnumerable<UserRole>> GetUserRoles(int userId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task SignIn(Users user, bool isPersistent = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String, "NonProfitCRM"));

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, "NonProfitCRM"));

            //var userRoles = user.UserRoleMapping.Select(r => r.Role);
            var userRoles = await _userService.GetUserRolesByUserId(user.Id);

            foreach (var roles in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, roles.Name));

            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedCustomer = user;
        }

        public virtual async Task SignOut()
        {
            //reset cached customer
            _cachedCustomer = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public virtual async Task<LoginResults> ValidateUser(LoginModel loginModel)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(usr => usr.Email == loginModel.UsernameOrEmail);

            if (user == null)
                return LoginResults.CustomerNotExist;

            var enteredPassword = _encryptionService.EncryptText(loginModel.Password);

            if (user.Password.Equals(enteredPassword))
                return LoginResults.Successful;

            return LoginResults.WrongPassword;
        }

        public virtual async Task<Users> GetAuthenticatedCustomer()
        {
            //whether there is a cached customer
            if (_cachedCustomer != null)
                return _cachedCustomer;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            Users customer = null;

                //try to get customer by email
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals("NonProfitCRM", StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    customer = await _unitOfWork.UsersRepository.GetFirstAsync(usr=>usr.Email == emailClaim.Value);

            if (customer == null)
                return null;

            //cache authenticated customer
            _cachedCustomer = customer;

            return _cachedCustomer;
        }
    }
}
