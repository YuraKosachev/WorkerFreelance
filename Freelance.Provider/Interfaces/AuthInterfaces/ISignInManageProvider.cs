using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance.Provider.EntityModels;
using Freelance.Provider.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Freelance.Provider.Interfaces
{
    public interface ISignInManageProvider : IManagerProvider
    {
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        Task<string> GetVerifiedUserIdAsync();
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> PassSignInAsync(LoginProviderModel model, bool shouldLockout);
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeProviderModel model);
    }
}
