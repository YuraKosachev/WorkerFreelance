using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Freelance.Web.Models;
using AutoMapper;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using Freelance.AppLogger;

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
    public class AppLoggerController : Controller
    {
        private ILogger Logger { get; set; }
        // GET: Logger
        [InjectionConstructor]
        public AppLoggerController(ILogger logger)
        {
            Logger = logger;
        }
        // GET: AppLogger
        public ActionResult Index(int? page)
        {
            try
            {
                if (page == null)
                    page = 1;
                var list = Logger.List().Select(item => Mapper.Map<LoggerViewModel>(item)).OrderByDescending(x=>x.DateTimeOfCreate).ToPagedList<LoggerViewModel>((int)page,10);
                return View(list);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
    }
}