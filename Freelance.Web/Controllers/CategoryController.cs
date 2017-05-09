using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freelance.Web.Models;
using PagedList.Mvc;
using PagedList;
using Freelance.Service;
using Freelance.Service.ServicesModel;
using Freelance.Service.Interfaces;
using AutoMapper;
using Freelance.FreelanceException;
using Microsoft.Practices.Unity;
using Freelance.Web.Extensions;
using Microsoft.AspNet.Identity;
using System.Xml.Linq;
using Freelance.AppLogger;

namespace Freelance.Web.Controllers
{

    public class CategoryControllerMapperProfile : Profile
    {
        public CategoryControllerMapperProfile()
        {
            CreateMap<CategoryServiceModel, CategoryViewModel>()
                .ForMember(item => item.CategoryId, exp => exp.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(item => item.Id, exp => exp.MapFrom(src => src.CategoryId));

        }

    }
    [ValidateInput(false)]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private ICategoryService Service { get; set; }
        private IAdminFileService FileService { get; set; }
        private ILogger Logger { get; set; }
        [InjectionConstructor]
        public CategoryController(ICategoryService service, IAdminFileService fileService,ILogger logger)
        {
            Service = service;
            FileService = fileService;
            Logger = logger;
        }



        // GET: Category
        public ActionResult Index(IndexState state)
        {
            try {
                var listSetting = Service.GetList();
                //add filter
                listSetting.Sort(state, "NameCategory").Page(state);
                var list = listSetting.StaticList<CategoryViewModel, CategoryServiceModel>(state);
                var pagginationList = new PagginationModelList<CategoryViewModel>(state, list);
              
                return View(pagginationList);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }

      

        // GET: Category/Create
        public ActionResult Create(IndexState state)
        {
            var item = new CategoryViewModel { IndexState = state };
            return View(item);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel model, IndexState indexState)
        {
            if (!ModelState.IsValid)
            {
                model.IndexState = indexState;
                return View(model);
            }

            try
            {
                if (model.Image != null)
                {
                    var fileName = FileService.Create(model.Image, User.Identity.GetUserId(), img => Convert.FromBase64String(img));
                    model.ImageName = fileName;
                }

                Service.Create(Mapper.Map<CategoryServiceModel>(model));
                return RedirectToAction("Index");
            }
            
                catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid id, IndexState state)
        {
            
            try
            {
                var category = Service.GetItem(id);
                 var model = Mapper.Map<CategoryViewModel>(category);
                model.IndexState = state;
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }


        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.Image != null)
                {
                    var fileName = FileService.Create(model.Image, User.Identity.GetUserId(), img => Convert.FromBase64String(img));
                    model.ImageName = fileName;
                }
                Service.Update(Mapper.Map<CategoryServiceModel>(model));
                return RedirectToAction("Index", model.IndexState);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                var item = Mapper.Map<CategoryViewModel>(Service.GetItem(id));
                return View(item);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }

        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id,IndexState indexState)
        {
            try
            {
                // TODO: Add delete logic here
                Service.Delete(id);
                return RedirectToAction("Index",indexState);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
    }
}
