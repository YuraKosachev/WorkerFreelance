namespace Freelance.Service.FileManagerConfg
{
    public class AppFolder
    {
        public string AppPath { get; set; }
        public string AdminFolder { get; set; }
        public string UserImagesFolder { get; set; }
        public string UserFilesFolder { get; set; }
    }
    public class FilePathConfiguration
    {
        public static AppFolder Folders { get; private set; }
        public static void SetPathConfiguration(AppFolder appFolders)
        {
            Folders = appFolders;
        }

    }
}