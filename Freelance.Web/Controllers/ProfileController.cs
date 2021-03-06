﻿using System;
using System.Web.Mvc;
using Freelance.Web.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Freelance.Service.ServicesModel;
using Freelance.Service.Interfaces;
using Freelance.FreelanceException;
using Microsoft.Practices.Unity;
using Freelance.AppLogger;
using Freelance.Web.Extensions;
using System.Xml.Linq;



namespace Freelance.Web.Controllers
{
    public class ProfileControllerMapperProfile : Profile
    {
        public ProfileControllerMapperProfile()
        {
            CreateMap<ProfileServiceModel, ProfileViewModel>()
                .ForMember(item => item.TimeAvailability, exp => exp.MapFrom(src => String.Format("{0:hh\\:mm} - {1:hh\\:mm}", src.TimeFrom, src.TimeTo))).ReverseMap();
            


        }

    }


    [ValidateInput(false)]

    public class ProfileController : Controller
    {

        private ICategoryService CategoryService { get; set; }
        private IProfileService ProfileService { get; set; }
        private ITextFilesService TextFileService { get; set; }
        private IPhotoService PhotoService { get; set; }
        private ILogger Logger { get; set; }

        [InjectionConstructor]
        public ProfileController(ICategoryService categoryService, IProfileService profileService, ITextFilesService textFileService,ILogger logger, IPhotoService photoService)
        {
            CategoryService = categoryService;
            ProfileService = profileService;
            TextFileService = textFileService;
            PhotoService = photoService;
            Logger = logger;
           
        }


        [Authorize(Roles = "client")]
        // GET: Profile
        public ActionResult Index(IndexState state)
        {
            try
            {
                var listSetting = ProfileService.GetList();
                if (!string.IsNullOrEmpty(state.SearchString))
                {
                    state.SearchString.Trim();
                    listSetting.Filter("User.UserFirstName.Contains(@0) OR User.UserSurname.Contains(@0) OR DescriptionProfile.Contains(@0)", state.SearchString);
                }
                if (state.TimeAvailability != null)
                {
                    listSetting.FilterAnd("TimeFrom <= @0 AND TimeTo >= @0", (TimeSpan)state.TimeAvailability);
                }
                if ((state.SortCategoryId != null) && (state.SortCategoryId != Guid.Empty))
                {
                    listSetting.FilterAnd("CategoryId == @0", (Guid)state.SortCategoryId);
                }
                state.Categories = CategoryService.Lookup();
                state.Categories.Add(Guid.Empty, "All");
                listSetting.Sort(state, "TimeFrom").Page(state);
                var list = listSetting.StaticList<ProfileViewModel, ProfileServiceModel>(state);
                var pagginationList = new PagginationModelList<ProfileViewModel>(state, list);
                return View(pagginationList);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
        [Authorize(Roles = "freelancer")]
        public ActionResult FreelancerProfiles(IndexState state)
        {
            try
            {
                var listSetting = ProfileService.GetList();
                listSetting.Filter("UserId == @0", User.Identity.GetUserId());
                listSetting.Sort(state, "TimeFrom").Page(state);
                var list = listSetting.StaticList<ProfileViewModel, ProfileServiceModel>(state);
                var pagginationList = new PagginationModelList<ProfileViewModel>(state, list);

                return View(pagginationList);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
        // GET: Profile/Details/5
        [Authorize(Roles = "freelancer, client")]
        public ActionResult Details(Guid id)
        {
            try
            {
                var item = ProfileService.GetItem(id);
                return View(Mapper.Map<ProfileViewModel>(item));
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }

        }
        [Authorize(Roles = "freelancer")]
        // GET: Profile/Create
        public ActionResult Create(IndexState state)
        {
            state.Categories = CategoryService.Lookup();
            var profile = new ProfileViewModel
            {
                IndexState = state
            };

            return View(profile);
        }
        [Authorize(Roles = "freelancer")]
        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(ProfileViewModel model, IndexState state)
        {
            model.UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                model.IndexState = state;
                model.IndexState.Categories = CategoryService.Lookup();
                return View(model);
            }
            try
            {
                // TODO: Add insert logic here

                if (Request.Files.Get(0).ContentLength > 0)
                {
                    var result = Request.GetFileData();
                    model.FileName = TextFileService.Create(result.FileContent, User.Identity.GetUserId(), result.FileExtension);
                }

                if (model.Image != null)
                {
                    var fileName = PhotoService.Create(model.Image, User.Identity.GetUserId(), img => Convert.FromBase64String(img));
                    model.ImageName = fileName;
                }
                ProfileService.Create(Mapper.Map<ProfileServiceModel>(model));

                return RedirectToAction("FreelancerProfiles", state);
            }
            catch(Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));

                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
        [Authorize(Roles = "freelancer")]
        // GET: Profile/Edit/5
        public ActionResult Edit(Guid id, IndexState state)
        {
            try
            {
                var item = Mapper.Map<ProfileViewModel>(ProfileService.GetItem(id));
                item.IndexState = state;
                item.IndexState.Categories = CategoryService.Lookup();
                return View(item);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }

        }
        [Authorize(Roles = "freelancer")]
        // POST: Profile/Edit/5
        [HttpPost]

        public ActionResult Edit(ProfileViewModel model, IndexState state)
        {
            model.UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                model.IndexState = state;
                model.IndexState.Categories = CategoryService.Lookup();
                return View(model);
            }
            try
            {
                if (Request.Files.Get(0).ContentLength > 0)
                {
                    var result = Request.GetFileData();
                    model.FileName = TextFileService.Create(result.FileContent, User.Identity.GetUserId(), result.FileExtension);
                }

                // TODO: Add update logic here
                ProfileService.Update(Mapper.Map<ProfileServiceModel>(model));
                return RedirectToAction("FreelancerProfiles", state);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
       
        [Authorize(Roles = "freelancer")]
        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, IndexState state)
        {
            try
            {
                // TODO: Add delete logic here
                ProfileService.Delete(id);
                return RedirectToAction("FreelancerProfiles", state);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));


                return RedirectToAction("Index", "Home", new { error = ex.Message });
            }
        }
        public FileResult GetFile(string fileName, string userId)
        {

            // Объект Stream
           
                string file_type = String.Format("application/{0}", FileHelperExtension.GetExtension(fileName));
                return File(TextFileService.GetFileStream(fileName, userId), file_type, fileName);
          
            
        }
       
    }
}
