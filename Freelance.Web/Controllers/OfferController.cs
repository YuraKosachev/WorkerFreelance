using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Freelance.Service.Interfaces;
using Freelance.Web.Models;
using AutoMapper;
using Freelance.Service.ServicesModel;
using Microsoft.AspNet.Identity;
using Freelance.Web.Extensions;
using Freelance.AppLogger;
using System.Xml.Linq;

namespace Freelance.Web.Controllers
{
    public class OfferControllerMapperProfile : Profile
    {
        public OfferControllerMapperProfile()
        {
            CreateMap<OfferViewModel, OfferServiceModel>()
                .ForMember(item => item.Date, exp => exp.MapFrom(src => src.Date.Add(src.Time)))
                .ReverseMap()
                .ForMember(item => item.Time, exp => exp.MapFrom(src => src.Date.ConvertToTimeSpan()));

        }

    }

    public class OfferController : Controller
    {
        private IOfferService OfferService { get; set; }
        private IProfileService ProfileService { get; set; }
        private ILogger Logger { get; set; }
        [InjectionConstructor]
        public OfferController(IOfferService offerService, IProfileService profileService,ILogger logger)
        {
            OfferService = offerService;
            ProfileService = profileService;
            Logger = logger;
        }


        // GET: Offer
        [Authorize(Roles = "freelancer, client")]
        public ActionResult Index(IndexState state)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var listSetting = OfferService.GetList();

                //filtring
                if (User.IsInRole("client"))
                    listSetting.Filter("UserId == @0", userId);
                if (User.IsInRole("freelancer"))
                    listSetting.Filter("Profile.UserId == @0", userId);


                listSetting.Sort(state, "DateOfCreate").Page(state);

                var list = listSetting.StaticList<OfferViewModel, OfferServiceModel>(state);
                var pagginationList = new PagginationModelList<OfferViewModel>(state, list);

                return View(pagginationList);
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));
                Response.StatusCode = 500;
                return Content(ex.Message);
            }

        }

        // GET: Offer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Offer/Create
        [HttpPost]
        public ActionResult Create(OfferViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                model.UserId = User.Identity.GetUserId();
                var offerId = OfferService.Create(Mapper.Map<OfferServiceModel>(model));
                return new JsonResult { Data = new { OfferId = offerId } };
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Confirm(OfferViewModel model)
        {
            try
            {
                // TODO: Add update logic here

                model.FreelancerConfirm = true;
                OfferService.Update(Mapper.Map<OfferServiceModel>(model));
                return new JsonResult { Data = new { OfferId = model.Id } };
            }
            catch (Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }
     
        // POST: Offer/Delete/5
        [Authorize(Roles = "client")]
        [HttpPost]
        public ActionResult Delete(Guid id, IndexState state)
        {
            try
            {
                // TODO: Add delete logic here
                OfferService.Delete(id);
                return RedirectToAction("Index", state);
            }
            catch(Exception ex)
            {
                Logger.Add(Mapper.Map<XElement>(LoggerViewModel.Instance(ex.GetType().ToString(), ex.Message, ex.StackTrace)));
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }
    }
}