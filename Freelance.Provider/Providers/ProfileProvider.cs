using Freelance.Provider.Interfaces;
using Freelance.Provider.EntityModels;
using Freelance.Extensions;

namespace Freelance.Provider.Providers
{
    public class ProfileProvider : FreelanceProvider<Profile>, IProfileProvider
    {
        public ProfileProvider() : base() { }
        public override Extensions.Interfaces.IAppQuery<Profile> GetList()
        {
            return new AppQuery<Profile>(Context.Set<Profile>().Include("Category").Include("User"));
        }
    }
}
