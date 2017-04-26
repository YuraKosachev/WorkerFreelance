using Microsoft.AspNet.Identity.EntityFramework;

namespace Freelance.Service.ServicesModel
{
    public class UserServiceModel : IdentityUser
    {
        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
    }
}
