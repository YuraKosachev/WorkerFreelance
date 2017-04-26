using System.Collections.Generic;
using System.Threading.Tasks;
using Freelance.Provider.EntityModels;
using Microsoft.AspNet.Identity;

namespace Freelance.Provider.Interfaces
{
    public interface IUserManageProvider : IManagerProvider
    {
        string GetUserFirstName(string name);
        Task<IdentityResult> AddToRoleAsync(string userId, string role);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> CreateAsync(User user);
        Task<User> FindByNameAsync(string name);

        Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId);
        Task<bool> IsEmailConfirmedAsync(string userId);
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
        Task<string> GetPhoneNumberAsync(string userId);
        Task<bool> GetTwoFactorEnabledAsync(string userId);
        Task<IdentityResult> RemoveLoginAsync(string userId, string loginProvider, string providerKey);
        Task<User> FindByIdAsync(string userId);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
        Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        User FindById(string userId);
        Task<IdentityResult> AddPasswordAsync(string userId, string newPassword);
        Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token);
        Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber);

    }
}
