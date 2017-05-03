using System;
using System.IO;
using System.Xml.Linq;


namespace Freelance.AppLogger
{
    public static class LoggerConf
    {
        public static string LoggerFullFilePath { get; private set; }
        private static string LoggerFileName { get; set; }
        private static string LoggerFilePath { get; set; }
        public static void SetConf(string filePath, string fileName)
        {
            LoggerFileName = fileName;
            LoggerFilePath = filePath;
            LoggerFullFilePath = PathGeneration();
            CheckFolder();
            CheckFile();

        }
        private static void CheckFolder()
        {
            var dirInfo = new DirectoryInfo(LoggerFilePath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
        private static string PathGeneration()
        {
            return String.Format(@"{0}\{1}", LoggerFilePath, LoggerFileName);
        }
        private static void CheckFile()
        {
            var fileInfo = new FileInfo(LoggerFullFilePath);
            if (!fileInfo.Exists)
            {
                var xDoc = new XDocument();
                var exceptions = new XElement("exceptions");
                xDoc.Add(exceptions);
                xDoc.Save(PathGeneration());
            }
        }
    }
}
