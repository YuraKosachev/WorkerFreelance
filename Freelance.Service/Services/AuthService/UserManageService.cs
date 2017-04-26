using System.Collections.Generic;
using System.Threading.Tasks;
using Freelance.Service.Interfaces.AuthServices;
using Freelance.Service.ServicesModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Freelance.Provider.EntityModels;
using Freelance.Provider.Interfaces;
using AutoMapper;
using Freelance.Provider.Providers;




namespace Freelance.Service.Services
{
    public class UserManageService : IUserManageService, IManagerService
    {

        private IUserManageProvider UserProvider { get; set; }
        public IOwinContext Context
        {
            get
            {
                return UserProvider.Context;
            }
            set
            {
                UserProvider.Context = value;
            }
        }
        public UserManageService()
        {
            UserProvider = new UserManageProvider();
        }

        public string GetUserFirstName(string name)
        {
            return UserProvider.GetUserFirstName(name);
        }
        //
        public Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            return UserProvider.AddToRoleAsync(userId, role);
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {

            return UserProvider.AddLoginAsync(userId, loginInfo);
        }

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            return UserProvider.ConfirmEmailAsync(userId, code);
        }

        public Task<IdentityResult> CreateAsync(UserServiceModel user, string password)
        {

            return UserProvider.CreateAsync(Mapper.Map<User>(user), password);
        }

        public Task<IdentityResult> CreateAsync(UserServiceModel user)
        {

            return UserProvider.CreateAsync(Mapper.Map<User>(user));
        }
        public async Task<UserServiceModel> FindByName(string name)
        {
            var user = await UserProvider.FindByNameAsync(name);

            return Mapper.Map<UserServiceModel>(user);
        }

        public Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            return UserProvider.GetValidTwoFactorProvidersAsync(userId);
        }
        public Task<bool> IsEmailConfirmedAsync(string userId)
        {
            return UserProvider.IsEmailConfirmedAsync(userId);
        }
        public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            return UserProvider.ResetPasswordAsync(userId, token, newPassword);
        }

        public Task<string> GetPhoneNumberAsync(string userId)
        {
            return UserProvider.GetPhoneNumberAsync(userId);
        }

        public Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            return UserProvider.GetTwoFactorEnabledAsync(userId);
        }
        public Task<IdentityResult> RemoveLoginAsync(string userId, string loginProvider, string providerKey)
        {
            return UserProvider.RemoveLoginAsync(userId, loginProvider, providerKey);
        }
        public async Task<UserServiceModel> FindByIdAsync(string userId)
        {
            var user = await UserProvider.FindByIdAsync(userId);

            return Mapper.Map<UserServiceModel>(user);
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            return UserProvider.GetLoginsAsync(userId);
        }

        public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            return UserProvider.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return UserProvider.ChangePasswordAsync(userId, oldPassword, newPassword);
        }
        public UserServiceModel FindById(string userId)
        {
            var user = UserProvider.FindById(userId);
            return Mapper.Map<UserServiceModel>(user);
        }
        public Task<IdentityResult> AddPasswordAsync(string userId, string newPassword)
        {
            return UserProvider.AddPasswordAsync(userId, newPassword);
        }

        public Task<IdentityResult> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
        {
            return UserProvider.ChangePhoneNumberAsync(userId, phoneNumber, token);
        }

        public Task<IdentityResult> SetPhoneNumberAsync(string userId, string phoneNumber)
        {
            return UserProvider.SetPhoneNumberAsync(userId, phoneNumber);
        }
    }
}