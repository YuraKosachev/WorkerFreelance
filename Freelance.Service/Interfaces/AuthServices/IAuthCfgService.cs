using Owin;

namespace Freelance.Service.Interfaces.AuthServices
{
    public interface IAuthCfgService
    {
        void Config(IAppBuilder app, string path);
        void ConfigureAuth();
    }
}
