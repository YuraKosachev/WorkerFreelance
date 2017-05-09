using Freelance.Provider.Interfaces;
using Freelance.Provider.EntityModels;
using Freelance.Extensions;

namespace Freelance.Provider.Providers
{
    public class OfferProvider : FreelanceProvider<Offer>, IOfferProvider
    {
        public OfferProvider() : base() { }
        public override Extensions.Interfaces.IAppQuery<Offer> GetList()
        {
            return new AppQuery<Offer>(Context.Set<Offer>().Include("Profile").Include("User").Include("Profile.Category").Include("Profile.User"));
        }

    }
}
