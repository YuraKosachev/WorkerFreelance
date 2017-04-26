using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance.Service.Interfaces;
using Microsoft.Practices.Unity;
using Freelance.FileManagerProvider.Interfaces;
using Freelance.Service.FileManagerConfg;

namespace Freelance.Service.Services
{
    public class AdminFileService : FreelanceFileService, IAdminFileService
    {
        [InjectionConstructor]
        public AdminFileService(IFileProvider fileProvider) : base(fileProvider)
        {
            Path = PathGeneration(Folders.AppPath, Folders.AdminFolder);
        }
        protected override string FolderPath(string folderName)
        {
            return Path;
        }
    }
}