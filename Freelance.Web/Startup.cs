using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Freelance.Web.Startup))]
namespace Freelance.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
