using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.Profile;
using Cinema.Web.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController()
        {
            _profileService = new ProfileService();
        }

        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            model.Filter = new ListFilterViewModel();
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }
            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _profileService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter); //todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Profile>(new List<Profile>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }
            // select lists

            return View(model);
        }

        [HttpPost]
        public ActionResult List(ListViewModel model)
        {

            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _profileService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter);//todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Profile>(new List<Profile>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

        public ActionResult Add()
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            //select lists

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            Business.Models.Profile.Profile profile = new Business.Models.Profile.Profile();
            profile.Code = model.Code;
            profile.Name = model.Name;
            var apiResponseModel = _profileService.Add("", profile);//todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(ProfileController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _profileService.GetById("", id); //todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var profile = apiResponseModel.Data;

            if (profile == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = profile.Id;
            model.Code = profile.Code;
            model.Name = profile.Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            var apiResponseModel = _profileService.GetById("", model.Id);//todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var profile = apiResponseModel.Data;

            if (profile == null)
            {
                return View("_ErrorNotExist");
            }

            profile.Code = model.Code;
            profile.Name = model.Name;

            var apiEditResponseModel = _profileService.Edit("", profile);//todo:token
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                // todo: select lists
                return View(model);
            }

            return RedirectToAction(nameof(ProfileController.List));
        }

        public ActionResult Delete(int id)
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _profileService.GetById("", id); //todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(ProfileController.List));
            }
            var profile = apiResponseModel.Data;

            if (profile == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(ProfileController.List));
            }
            var apiDeleteResponseModel = _profileService.Delete("", id); //todo:token
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(ProfileController.List));
            }

            return RedirectToAction(nameof(ProfileController.List));
        }


    }
}