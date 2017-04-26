using AutoMapper;
using System.Threading.Tasks;
using Freelance.Provider.EntityModels;
using Freelance.Service.Interfaces.AuthServices;
using Microsoft.AspNet.Identity.Owin;
using Freelance.Service.ServicesModel;
using Freelance.Provider.Interfaces;
using Microsoft.Owin;
using Freelance.Provider.Providers;

namespace Freelance.Service.Services
{
    public class AuthServiceMapperProfile : AutoMapper.Profile
    {
        public AuthServiceMapperProfile()
        {
            CreateMap<LoginServiceModel, LoginProviderModel>();
            CreateMap<VerifyCodeServiceModel, VerifyCodeProviderModel>();
            CreateMap<UserServiceModel, User>().ReverseMap();
        }

    }

    public class SignInManageService : ISignInManageService
    {

        private ISignInManageProvider SignInManager { get; set; }
        public IOwinContext Context
        {
            get
            {
                return SignInManager.Context;
            }

            set
            {
                SignInManager.Context = value;
            }
        }
        public SignInManageService()
        {
            SignInManager = new SignInManageProvider();
            //SignInManager = new ProviderFactory().SignInManageProvider;
        }

        public Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            return SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }
        public Task<string> GetVerifiedUserIdAsync()
        {
            return SignInManager.GetVerifiedUserIdAsync();
        }

        public Task<bool> HasBeenVerifiedAsync()
        {
            return SignInManager.HasBeenVerifiedAsync();
        }
        public Task<SignInStatus> PassSignInAsync(LoginServiceModel model, bool shouldLockout)
        {

            return SignInManager.PassSignInAsync(Mapper.Map<LoginProviderModel>(model), shouldLockout: false);
        }
        public Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            return SignInManager.SendTwoFactorCodeAsync(provider);
        }

        public Task SignInAsync(UserServiceModel user, bool isPersistent, bool rememberBrowser)
        {
            return SignInManager.SignInAsync(Mapper.Map<User>(user), isPersistent: false, rememberBrowser: false);
        }

        public Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeServiceModel model)
        {
            return SignInManager.TwoFactorSignInAsync(Mapper.Map<VerifyCodeProviderModel>(model));
        }
    }
}