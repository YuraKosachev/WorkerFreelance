using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;

namespace Freelance.Web.Extensions.IdentityExtension
{
    public static  class IdExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            return String.Empty;
        }
    }
}