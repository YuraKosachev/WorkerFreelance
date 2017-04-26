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
    public class TextFileService : FreelanceFileService, ITextFilesService
    {
        [InjectionConstructor]
        public TextFileService(IFileProvider fileProvider) : base(fileProvider)
        {
            Path = PathGeneration(Folders.AppPath, Folders.UserFilesFolder);
        }
    }
}
