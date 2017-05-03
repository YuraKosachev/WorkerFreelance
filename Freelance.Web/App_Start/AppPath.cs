using System.Web;
using System.Web.Configuration;
using Freelance.Service.FileManagerConfg;

namespace Freelance.Web
{
    public class AppPath
    {
        public static void SetAppPath()
        {
            var folderConfig = new AppFolder();
            folderConfig.AppPath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ImageStoreFolder"]);
            folderConfig.AdminFolder = "app";
            folderConfig.UserFilesFolder = "textfiles";
            folderConfig.UserImagesFolder = "photos";
            FilePathConfiguration.SetPathConfiguration(folderConfig);
           
        }
    }
}