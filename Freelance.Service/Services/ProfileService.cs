using System;
using Freelance.Service.ServicesModel;
using Freelance.Provider.EntityModels;
using Freelance.Service.Interfaces;
using Freelance.Provider.Interfaces;
using mapper = AutoMapper;
using Microsoft.Practices.Unity;
namespace Freelance.Service.Services
{
    public class ProfileServiceMapperProfile : mapper.Profile
    {
        public ProfileServiceMapperProfile()
        {
            CreateMap<ProfileServiceModel, Profile>().ReverseMap()
                .ForMember(item => item.CategoryName, exp => exp.MapFrom(src => src.Category.NameCategory))
                .ForMember(item => item.FreelancerName, exp => exp.MapFrom(src => String.Format("{0} {1}", src.User.UserFirstName, src.User.UserSurname)))
                .ForMember(item => item.PhoneNumber, exp => exp.MapFrom(src => src.User.PhoneNumber))
                .ForMember(item => item.ImageName, exp => exp.MapFrom(src => src.Category.ImageName))
                 .ForMember(item => item.CategoryDescription, exp => exp.MapFrom(src => src.Category.DescriptionCategory));


        }

    }


    public class ProfileService : FreelanceService<ProfileServiceModel, Profile>, IProfileService
    {
        [InjectionConstructor]
        public ProfileService(IProfileProvider provider) : base(provider) { }

    }
}
