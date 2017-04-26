using System.Threading.Tasks;
using Freelance.Provider.EntityModels;
using Freelance.Provider.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace Freelance.Provider.Providers
{
    public class SignInManageProvider : AuthProvider<FreelanceSignInManager>, ISignInManageProvider
    {
        public SignInManageProvider() : base() { }
        public SignInManageProvider(IOwinContext context, FreelanceSignInManager userManager)
            : base(context, userManager)
        { }



        public Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            return Manager.ExternalSignInAsync(loginInfo, isPersistent: false);
        }
        public Task<string> GetVerifiedUserIdAsync()
        {
            return Manager.GetVerifiedUserIdAsync();
        }

        public Task<bool> HasBeenVerifiedAsync()
        {
            return Manager.HasBeenVerifiedAsync();
        }
        public Task<SignInStatus> PassSignInAsync(LoginProviderModel model, bool shouldLockout)
        {
            return Manager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        }
        public Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            return Manager.SendTwoFactorCodeAsync(provider);
        }

        public Task SignInAsync(User user, bool isPersistent, bool rememberBrowser)
        {
            return Manager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

        public Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeProviderModel model)
        {
            return Manager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        }
    }
}