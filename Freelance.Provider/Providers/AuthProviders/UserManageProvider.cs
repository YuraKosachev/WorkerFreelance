using System.Collections.Generic;
using System.Threading.Tasks;
using Freelance.Provider.EntityModels;
using Freelance.Provider.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace Freelance.Provider.Providers
{
    public class UserManageProvider : AuthProvider<FreelanceUserManager>, IUserManageProvider
    {

        public UserManageProvider() : base() { }
        public UserManageProvider(IOwinContext context, FreelanceUserManager userManager)
            : base(context, userManager)
        { }

        public string GetUserFirstName(string name)
        {
            return Manager.FindByName(name).UserFirstName;
        }
        //
        public Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            return Manager.AddToRoleAsync(userId, role);
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {

            return Manager.AddLoginAsync(userId, loginInfo);
        }

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            return Manager.ConfirmEmailAsync(userId, code);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return Manager.CreateAsync(user, password);
        }

        public Task<IdentityResult> CreateAsync(User user)
        {
            return Manager.CreateAsync(user);
        }
        public Task<User> FindByNameAsync(string name)
        {
            return Manager.FindByNameAsync(name);
        }

        public Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            return Manager.GetValidTwoFactorProvidersAsync(userId);
        }
        public Task<bool> IsEmailConfirmedAsync(string userId)
        {
            return Manager.IsEmailConfirmedAsync(userId);
        }
        public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            return Manager.ResetPasswordAsync(userId, token, newPassword);
        }

        public Task<string> GetPhoneNumberAsync(string userId)
        {
            return Manager.GetPhoneNumberAsync(userId);
        }

        public Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            return Manager.GetTwoFactorEnabledAsync(userId);
        }
        public Task<IdentityResult> RemoveLoginAsync(string userId, string loginProvider, string providerKey)
        {
            return Manager.RemoveLoginAsync(userId, new UserLoginInfo(loginProvider, providerKey));
        }
        public Task<User> FindByIdAsync(string userId)
        {
            return Manager.FindByIdAsync(userId);
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            return Manager.GetLoginsAsync(userId);
        }

        public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            return Manager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return Manager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }
        public User FindById(string userId)
        {
            return Manager.FindById(userId);
        }
        public Task<IdentityResult> AddPasswordAsync(string userId, string newPassword)
        {
            return Manager.AddPasswordAsync(userId, newPassword);
        }

        public Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
        {
            return Manager.ChangePhoneNumberAsync(userId, phoneNumber, token);
        }

        public Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber)
        {
            return Manager.SetPhoneNumberAsync(userId, phoneNumber);
        }
    }
}