using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Freelance.Service.Interfaces;
using Microsoft.Practices.Unity;
using Freelance.Web.Models;
using System;
using Freelance.AppLogger;
using System.Xml.Linq;

namespace Freelance.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger Logger { get; set; }
        private ICategoryService CategoryService { get; set; }

        [InjectionConstructor]
        public HomeController(ICategoryService categoryService,ILogger logger)
        {
            CategoryService = categoryService;
            Logger = logger;
        }
        public ActionResult Index()
        {
            try {
                var list = CategoryService.GetList().List().Select(item => Mapper.Map<CategoryViewModel>(item)).ToList();
                return View(list);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

    
    }
}