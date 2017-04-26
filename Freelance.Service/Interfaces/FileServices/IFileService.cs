using System;
using System.IO;


namespace Freelance.Service.Interfaces
{
    public interface IFileService
    {
        string Create(string base64Content, string userId, Func<string, byte[]> Convert);
        string Create(byte[] content, string userId, string fileExtension);
        string GetFileString(string fileName, string userId,Func<byte[],string> Convert);
        FileStream GetFileStream(string fileName, string userId);
        byte[] GetFileBytes(string fileName, string userId);
        void Delete(string fileName, string userId);
    }
}

