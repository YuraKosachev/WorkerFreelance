using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Freelance.Service;
using Freelance.Service.ServicesModel;
using Freelance.Service.Interfaces;
using Microsoft.Practices.Unity;
using Freelance.Web.Models;

namespace Freelance.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService CategoryService { get; set; }

        [InjectionConstructor]
        public HomeController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }
        public ActionResult Index()
        {
            var list = CategoryService.GetList().List().Select(item=>Mapper.Map<CategoryViewModel>(item)).ToList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}