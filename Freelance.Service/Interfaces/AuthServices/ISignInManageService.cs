using System.Threading.Tasks;
using Freelance.Service.ServicesModel;
using Microsoft.AspNet.Identity.Owin;

namespace Freelance.Service.Interfaces.AuthServices
{
    public interface ISignInManageService : IManagerService
    {
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        Task<string> GetVerifiedUserIdAsync();
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> PassSignInAsync(LoginServiceModel model, bool shouldLockout);
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task SignInAsync(UserServiceModel user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeServiceModel model);
    }
}
