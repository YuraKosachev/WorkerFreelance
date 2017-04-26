using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Freelance.Web.Extensions
{
    public class FileData
    {
        public string FileExtension { get; set; }
        public byte[] FileContent { get; set; }
    }
    public static class FileHelperExtension
    {
        public static FileData GetFileData(this HttpRequestBase request)
        {
            var file = new FileData();
            var result = request.Files.Get(0);
            file.FileExtension = GetExtension(result.FileName);
            file.FileContent = new byte[result.InputStream.Length];
            using (Stream stream = result.InputStream)
            {
                stream.Read(file.FileContent, 0, file.FileContent.Length);
            }

            return file;
        }
        public static string GetExtension(string fileName)
        {

            return fileName.Split('.').Last();
        }
    }
}