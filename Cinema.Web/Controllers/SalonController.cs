using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.Salon;
using Cinema.Web.Filters;
using Cinema.Web.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class SalonController : Controller
    {

        private readonly ISalonService _salonService;
        public SalonController()
        {
            _salonService = new SalonService();
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_LIST)]
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
            SalonSearchFilter searchFilter = new SalonSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _salonService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Salon>(new List<Salon>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }
            // select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_LIST)]
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

            SalonSearchFilter searchFilter = new SalonSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _salonService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Salon>(new List<Salon>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_ADD)]
        public ActionResult Add()
        {
            Models.Salon.AddViewModel model = new AddViewModel();
            //select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Salon.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }
            Business.Models.Salon.Salon salon = new Business.Models.Salon.Salon();
            salon.Name = model.Name;
            var apiResponseModel = _salonService.Add(SessionHelper.CurrentUser.UserToken, salon);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(SalonController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Salon.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _salonService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var salon = apiResponseModel.Data;

            if (salon == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = salon.Id;
            model.Name = salon.Name;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Salon.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            var apiResponseModel = _salonService.GetById(SessionHelper.CurrentUser.UserToken, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var salon = apiResponseModel.Data;

            if (salon == null)
            {
                return View("_ErrorNotExist");
            }

            salon.Name = model.Name;

            var apiEditResponseModel = _salonService.Edit(SessionHelper.CurrentUser.UserToken, salon);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                // select lists
                return View(model);
            }

            return RedirectToAction(nameof(SalonController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.SALON_DELETE)]
        public ActionResult Delete(int id)
        {
            Models.Salon.AddViewModel model = new AddViewModel();
            //select lists

            var apiResponseModel = _salonService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(SalonController.List));
            }
            var salon = apiResponseModel.Data;

            if (salon == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(SalonController.List));
            }
            var apiDeleteResponseModel = _salonService.Delete(SessionHelper.CurrentUser.UserToken, id);
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(SalonController.List));
            }

            return RedirectToAction(nameof(SalonController.List));
        }
    }
}