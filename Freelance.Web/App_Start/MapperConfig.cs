using AutoMapper;
using Freelance.Web.Controllers;
using Freelance.Service.Services;

namespace Freelance.Web
{
    public static class MapperConfig
    {

        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AuthControllerMapperProfile>();
                cfg.AddProfile<AuthServiceMapperProfile>();
                cfg.AddProfile<CategoryControllerMapperProfile>();
                cfg.AddProfile<ProfileControllerMapperProfile>();
                cfg.AddProfile<OfferControllerMapperProfile>();
                cfg.AddProfile<CategoryServiceMapperProfile>();
                cfg.AddProfile<OfferServiceMapperProfile>();
                cfg.AddProfile<ProfileServiceMapperProfile>();
                cfg.AddProfile<LoggerControllerMapperProfile>();
            });
        }
    }
}