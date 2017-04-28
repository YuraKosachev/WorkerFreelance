using System;
using Freelance.Service.ServicesModel;
using Freelance.Provider.EntityModels;
using Freelance.Service.Interfaces;
using Freelance.Provider.Interfaces;
using mapper = AutoMapper;
using Microsoft.Practices.Unity;

namespace Freelance.Service.Services
{
    public class OfferServiceMapperProfile : mapper.Profile
    {
        public OfferServiceMapperProfile()
        {
            CreateMap<OfferServiceModel, Offer>()
                .ReverseMap()
                .ForMember(item => item.FreelancerName, exp => exp.MapFrom(src => String.Format("{0} {1}", src.User.UserFirstName, src.User.UserSurname)))
                .ForMember(item => item.PhoneNumber, exp => exp.MapFrom(src => src.User.PhoneNumber))
                .ForMember(item => item.NameCategory, exp => exp.MapFrom(src => src.Profile.Category.NameCategory))
                .ForMember(item => item.DescriptionCategory, exp => exp.MapFrom(src => src.Profile.Category.DescriptionCategory))
                .ForMember(item => item.CategoryImageName, exp => exp.MapFrom(src => src.Profile.Category.ImageName));


        }

    }
    public class OfferService : FreelanceService<OfferServiceModel, Offer>, IOfferService
    {
        [InjectionConstructor]
        public OfferService(IOfferProvider provider) : base(provider) { }

    }
}
