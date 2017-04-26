using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Freelance.Web.Models;
using Freelance.Service.Interfaces.AuthServices;


using Freelance.Service.ServicesModel;


namespace Freelance.Web.Controllers
{
    public class AuthControllerMapperProfile : AutoMapper.Profile
    {
        public AuthControllerMapperProfile()
        {
            CreateMap<VerifyCodeViewModel, VerifyCodeServiceModel>();
            CreateMap<LoginViewModel, LoginServiceModel>();
            CreateMap<RegisterViewModel, UserServiceModel>()
                .ForMember(item => item.UserName, exp => exp.MapFrom(src => src.Email))
                .ForMember(item => item.UserSurname, exp => exp.MapFrom(src => src.Surname))
                .ForMember(item => item.UserFirstName, exp => exp.MapFrom(src => src.Name));
            CreateMap<ExternalLoginConfirmationViewModel, UserServiceModel>()
                .ForMember(item => item.UserName, exp => exp.MapFrom(src => src.Email))
                .ForMember(item => item.Email, exp => exp.MapFrom(src => src.Email));
        }

    }

    public abstract class AuthController : Controller
    {
        protected IUserManageService _userManageService;
        protected ISignInManageService _signInManageService;
        protected IUserManageService UserManageService
        {
            get
            {
                _userManageService.Context = GetContext();
                return _userManageService;
            }
            set
            {
                _userManageService = value;
            }
        }

        protected ISignInManageService SignInManageService
        {
            get
            {
                _signInManageService.Context = GetContext();
                return _signInManageService;
            }
            set
            {
                _signInManageService = value;
            }
        }


        public AuthController(IUserManageService userManager, ISignInManageService signInManager)
        {
            UserManageService = userManager;
            SignInManageService = signInManager;
        }


        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        protected const string XsrfKey = "XsrfId";

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        protected Microsoft.Owin.IOwinContext GetContext()
        {
            return HttpContext.GetOwinContext();
        }
    }
}