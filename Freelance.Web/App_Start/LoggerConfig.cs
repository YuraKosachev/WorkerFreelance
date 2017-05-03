using System;
using Freelance.AppLogger;
using System.Web;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;


namespace Freelance.Web
{
    public class LoggerConfig
    {
        public static void Config()
        {
            var fileName = "applogger.xml";
            var loggerPath = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["LoggerStoreFolder"]);
            LoggerConf.SetConf(loggerPath, fileName);

        }
    }
}