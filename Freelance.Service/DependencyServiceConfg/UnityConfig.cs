using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Freelance.Provider.Interfaces;
using Freelance.Provider.Providers;
using Freelance.Service.Services;
using Freelance.Service.Interfaces;
using Freelance.Service.Interfaces.AuthServices;
using Freelance.FileManagerProvider.Interfaces;
using Freelance.FileManagerProvider.RepositoryProviders;

namespace Freelance.Service.DependencyServiceConfg
{

    public class UnityDependencyConfig
    {

        public static void RegisterTypes(IUnityContainer container)
        {


            //Provider dependency
            container.RegisterType<IUserManageProvider, UserManageProvider>();
            container.RegisterType<ISignInManageProvider, SignInManageProvider>();
            container.RegisterType<IProfileProvider, ProfileProvider>();
            container.RegisterType<ICategoryProvider, CategoryProvider>();
            container.RegisterType<IOfferProvider, OfferProvider>();
            //file repository
            container.RegisterType<IFileProvider, FreelanceFileProvider>();
        
        }
    }
}
