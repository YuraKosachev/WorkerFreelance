using Owin;
using Freelance.Service.AuthConfg;

namespace Freelance.Web
{
    public partial class Startup
    {
        // Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            FreelanceAuthConfg.ConfigureAuth(app, "/Account/Login");

        }
    }
}