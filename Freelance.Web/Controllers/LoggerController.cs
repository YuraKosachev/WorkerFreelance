using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Freelance.Web.Models;
using Freelance.AppLogger;
using Microsoft.Practices.Unity;
using System.Xml.Linq;
using System.Web.Mvc;

namespace Freelance.Web.Controllers
{
    public class LoggerControllerMapperProfile : Profile
    {
        public LoggerControllerMapperProfile()
        {
            CreateMap<LoggerViewModel, XElement>()
                .ConvertUsing(dest => {
                    var exception = new XElement("exception");
                    var exceptionId = new XAttribute("id", dest.ExceptionId);
                    var exceptionDate = new XAttribute("date", dest.DateTimeOfCreate);
                    var exceptionType = new XAttribute("type", dest.ExceptionType);
                    var exceptionMessage = new XElement("message", dest.ExceptionMessage);
                    var exceptionStackTrace = new XElement("trace", dest.ExceptionStackTrace);
                    exception.Add(exceptionId, exceptionDate, exceptionType, exceptionMessage, exceptionStackTrace);
                    return exception;
                });
            CreateMap<XElement, LoggerViewModel>()
            .ConstructUsing(dest => {
                var item = new LoggerViewModel();
                item.ExceptionId = Guid.Parse(dest.Attribute("id").Value);
                item.DateTimeOfCreate = DateTime.Parse(dest.Attribute("date").Value);
                item.ExceptionType = dest.Attribute("type").Value;
                item.ExceptionMessage = dest.Element("message").Value;
                item.ExceptionStackTrace = dest.Element("trace").Value;
                return item;

            });

        }
    }

    [Authorize(Roles = "admin")]
    public class LoggerController : Controller
    {
        private ILogger Logger { get; set; }
        // GET: Logger
        [InjectionConstructor]
        public LoggerController(ILogger logger)
        {
            Logger = logger;
        }
        public ActionResult Index()
        {
            var list = Logger.List().Select(item => Mapper.Map<LoggerViewModel>(item));
            return View(list);
        }
        
    }
}