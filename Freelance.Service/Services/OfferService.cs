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
                .ForMember(item => item.UserName, exp => exp.MapFrom(src => String.Format("{0} {1}", src.User.UserFirstName, src.User.UserSurname)))
                .ForMember(item => item.FreelancerName, exp => exp.MapFrom(src => String.Format("{0} {1}", src.Profile.User.UserFirstName, src.Profile.User.UserSurname)))
                .ForMember(item => item.PhoneNumber, exp => exp.MapFrom(src => src.User.PhoneNumber))
                .ForMember(item => item.NameCategory, exp => exp.MapFrom(src => src.Profile.Category.NameCategory))
                .ForMember(item => item.DescriptionCategory, exp => exp.MapFrom(src => src.Profile.Category.DescriptionCategory))
                .ForMember(item => item.ImageName, exp => exp.MapFrom(src => src.Profile.ImageName))
                .ForMember(item => item.FreelancerId, exp => exp.MapFrom(src => src.Profile.UserId)); 


        }

    }
    public class OfferService : FreelanceService<OfferServiceModel, Offer>, IOfferService
    {
        [InjectionConstructor]
        public OfferService(IOfferProvider provider) : base(provider) { }

    }
}
