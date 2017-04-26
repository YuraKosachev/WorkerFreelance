using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance.FileManagerProvider.Interfaces;
using System.IO;
using Freelance.FreelanceException;

namespace Freelance.FileManagerProvider.RepositoryProviders
{
    public class FreelanceFileProvider : IFileProvider
    {
        public string Create(byte[] content, string extension, Func<string, string> pathGenerator)
        {
            var fileName = FileName(Guid.NewGuid(), extension);
            using (FileStream stream = new FileStream(pathGenerator(fileName), FileMode.Create))
            {
                stream.Write(content, 0, content.Length);
            }
            return fileName;
        }

        public void Delete(string path)
        {
            
            if(!File.Exists(path))
                throw new ItemNotFoundException("Файл не найден");
            
            File.Delete(path);
        }


        public FileStream GetFile(string path)
        {
            if (!File.Exists(path))
                throw new ItemNotFoundException("Файл не найден");
            return new FileStream(path,FileMode.Open);
        }

        private string FileName<TType>(TType name, string extension)
        {
            return String.Format("{0}.{1}", name, extension);
        }
    }
}

