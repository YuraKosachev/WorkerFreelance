using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance.Service.Interfaces;
using Microsoft.Practices.Unity;
using Freelance.FileManagerProvider.Interfaces;

namespace Freelance.Service.Services
{
    public class UserPhotoService : FreelanceFileService, IPhotoService
    {
        [InjectionConstructor]
        public UserPhotoService(IFileProvider fileProvider) : base(fileProvider)
        {
            Path = PathGeneration(Folders.AppPath, Folders.UserImagesFolder);
        }
    }
}
