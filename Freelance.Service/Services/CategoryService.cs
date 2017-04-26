using System;
using System.Collections.Generic;
using Freelance.Service.ServicesModel;
using Freelance.Provider.EntityModels;
using Freelance.Provider.Interfaces;
using Freelance.Service.Interfaces;
using mapper = AutoMapper;
using Microsoft.Practices.Unity;

namespace Freelance.Service.Services
{
    public class CategoryServiceMapperProfile : mapper.Profile
    {
        public CategoryServiceMapperProfile()
        {
            CreateMap<CategoryServiceModel, Category>().ReverseMap();


        }

    }

    public class CategoryService : FreelanceService<CategoryServiceModel, Category>, ICategoryService
    {
        [InjectionConstructor]
        public CategoryService(ICategoryProvider provider) : base(provider) { }



        public IDictionary<Guid, string> Lookup()
        {
            return (Provider as ICategoryProvider).Lookup();
        }
    }
}
