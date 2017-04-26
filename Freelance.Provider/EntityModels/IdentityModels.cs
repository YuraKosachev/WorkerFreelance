using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Freelance.Provider.EntityModels
{
    public class User : IdentityUser
    {

        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        public string ImageName { get; set; }
        //navi setting
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}