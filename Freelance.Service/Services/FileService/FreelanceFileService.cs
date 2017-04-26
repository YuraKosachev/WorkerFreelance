using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Freelance.Service.Interfaces;
using Freelance.FileManagerProvider.Interfaces;
using Freelance.Service.FileManagerConfg;

namespace Freelance.Service.Services
{
    public abstract class FreelanceFileService : IFileService
    {
        protected string Path { get; set; }
        protected IFileProvider FileProvider { get; set; }
        protected AppFolder Folders { get; set; }

        public FreelanceFileService(IFileProvider provider)
        {
            FileProvider = provider;
            Folders = FilePathConfiguration.Folders;
        }
        public string Create(byte[] content, string userId, string fileExtension)
        {
            var path = FolderPath(userId);
            Сheck(path);
            return FileProvider.Create(content, fileExtension, file => PathGeneration(path, file));
        }

        public string Create(string base64Content, string userId, Func<string, byte[]> Convert)
        {
            byte[] content = Convert(Base64ImageContent(base64Content));
            return Create(content, userId, "png");
        }

        public void Delete(string fileName, string userId)
        {
            FileProvider.Delete(PathGeneration(FolderPath(userId),fileName));
        }

        protected virtual string FolderPath(string folderName)
        {
            return PathGeneration(Path, folderName);
        }
        protected string PathGeneration(string path, string name)
        {
            return String.Format(@"{0}\{1}", path, name);
        }
        protected string Base64ImageContent(string base64Content)
        {
            return base64Content.Replace("data:image/png;base64,", "");
        }
        protected void Сheck(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public string GetFileString(string fileName, string userId, Func<byte[],string> Convert)
        {
            return Convert(GetFileBytes(fileName, userId));
        }

        public FileStream GetFileStream(string fileName, string userId)
        {
            return FileProvider.GetFile(PathGeneration(FolderPath(userId), fileName));
        }

        public byte[] GetFileBytes(string fileName, string userId)
        {
            using (FileStream stream = GetFileStream(fileName, userId))
            {
                byte[] array = new byte[stream.Length];
                stream.Write(array, 0, array.Length);
                return array;
            }
            
        }
    }
}

