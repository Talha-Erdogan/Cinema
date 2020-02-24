using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.Seance;
using Cinema.Web.Filters;
using Cinema.Web.Models.Seance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class SeanceController : Controller
    {
        private readonly ISeanceService _seanceService;
        public SeanceController()
        {
            _seanceService = new SeanceService();
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_LIST)]
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
            SeanceSearchFilter searchFilter = new SeanceSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _seanceService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter); 
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Seance>(new List<Seance>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }
            // select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_LIST)]
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

            SeanceSearchFilter searchFilter = new SeanceSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _seanceService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Seance>(new List<Seance>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_ADD)]
        public ActionResult Add()
        {
            Models.Seance.AddViewModel model = new AddViewModel();
            //select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Seance.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }
            Business.Models.Seance.Seance seance = new Business.Models.Seance.Seance();
            seance.Name = model.Name;
            seance.Date = model.Date;
            seance.Time = model.Time;
            var apiResponseModel = _seanceService.Add(SessionHelper.CurrentUser.UserToken, seance);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(SeanceController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Seance.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _seanceService.GetById(SessionHelper.CurrentUser.UserToken, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var seance = apiResponseModel.Data;

            if (seance == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = seance.Id;
            model.Name = seance.Name;
            model.Date = seance.Date;
            model.Time = seance.Time;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Seance.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            var apiResponseModel = _seanceService.GetById(SessionHelper.CurrentUser.UserToken, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var seance = apiResponseModel.Data;

            if (seance == null)
            {
                return View("_ErrorNotExist");
            }

            seance.Name = model.Name;
            seance.Date = model.Date;
            seance.Time = model.Time;

            var apiEditResponseModel = _seanceService.Edit(SessionHelper.CurrentUser.UserToken, seance);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                //  select lists
                return View(model);
            }

            return RedirectToAction(nameof(SeanceController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.SEANCE_DELETE)]
        public ActionResult Delete(int id)
        {
            Models.Seance.AddViewModel model = new AddViewModel();
            //select lists

            var apiResponseModel = _seanceService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(SeanceController.List));
            }
            var seance = apiResponseModel.Data;

            if (seance == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(SeanceController.List));
            }
            var apiDeleteResponseModel = _seanceService.Delete(SessionHelper.CurrentUser.UserToken, id);
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(SeanceController.List));
            }

            return RedirectToAction(nameof(SeanceController.List));
        }




    }
}