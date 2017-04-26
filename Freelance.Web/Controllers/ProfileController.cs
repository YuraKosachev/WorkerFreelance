using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Freelance.Web.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using AutoMapper;
using Freelance.Service;
using Freelance.Service.ServicesModel;
using Freelance.Service.Interfaces;
using Freelance.Service.Services;
using Freelance.FreelanceException;
using Microsoft.Practices.Unity;
using Freelance.Web.Extensions;
//----------------

using System.IO;


namespace Freelance.Web.Controllers
{
    public class ProfileControllerMapperProfile : Profile
    {
        public ProfileControllerMapperProfile()
        {
            CreateMap<ProfileServiceModel, ProfileViewModel>()
                .ForMember(item => item.TimeAvailability, exp => exp.MapFrom(src => String.Format("{0} - {1}", src.TimeFrom, src.TimeTo))).ReverseMap();
            


        }

    }




    public class ProfileController : Controller
    {

        private ICategoryService CategoryService { get; set; }
        private IProfileService ProfileService { get; set; }
        private ITextFilesService TextFileService { get; set; }

        [InjectionConstructor]
        public ProfileController(ICategoryService categoryService, IProfileService profileService, ITextFilesService textFileService)
        {
            CategoryService = categoryService;
            ProfileService = profileService;
            TextFileService = textFileService;
        }


        [Authorize(Roles = "client")]
        // GET: Profile
        public ActionResult Index(IndexState state)
        {
            var listSetting = ProfileService.GetList();
            if (!string.IsNullOrEmpty(state.SearchString))
            {
                state.SearchString.Trim();
                listSetting.Filter("User.UserFirstName.Contains(@0) OR User.UserSurname.Contains(@0) OR DescriptionProfile.Contains(@0)", state.SearchString);
            }
            if (state.TimeAvailability != null)
            {
                listSetting.FilterAnd("TimeFrom <= @0 AND TimeTo >= @0", (TimeSpan)state.TimeAvailability);//"TimeFrom <= @0 AND TimeTo >= @0", (TimeSpan)state.TimeAvailabilityFilter);
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
        [Authorize(Roles = "freelancer")]
        public ActionResult FreelancerProfiles(IndexState state)
        {
            var listSetting = ProfileService.GetList();
            listSetting.Filter("UserId == @0", User.Identity.GetUserId());
            listSetting.Sort(state, "TimeFrom").Page(state);
            var list = listSetting.StaticList<ProfileViewModel, ProfileServiceModel>(state);
            var pagginationList = new PagginationModelList<ProfileViewModel>(state, list);

            return View(pagginationList);
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
            catch (ItemNotFoundException e)
            {
                return View();
            }
            catch
            {
                return View();
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


                ProfileService.Create(Mapper.Map<ProfileServiceModel>(model));

                return RedirectToAction("FreelancerProfiles", state);
            }
            catch
            {
                return View();
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
            catch (ItemNotFoundException e)
            {
                return View();
            }
            catch (Exception e)
            {
                return View();
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
            catch
            {
                return View();
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
            catch
            {
                return View();
            }
        }
        public FileResult GetFile(string fileName, string userId)
        {
            
            // Объект Stream
           
            string file_type = String.Format("application/{0}", FileHelperExtension.GetExtension(fileName));
            return File(TextFileService.GetFileStream(fileName,userId), file_type, fileName);
        }
       
    }
}
