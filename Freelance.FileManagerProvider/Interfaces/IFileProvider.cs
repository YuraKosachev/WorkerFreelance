using System;
using System.IO;


namespace Freelance.FileManagerProvider.Interfaces
{
    public interface IFileProvider
    {
        string Create(byte[] content, string fileExtension, Func<string, string> pathGenerator);
        FileStream GetFile(string path);
        void Delete(string path);
    }
}
