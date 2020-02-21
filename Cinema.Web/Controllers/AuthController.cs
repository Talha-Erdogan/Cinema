using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Auth;
using Cinema.Web.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        public AuthController()
        {
            _authService = new AuthService();
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
            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            
            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Auth>(new List<Auth>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
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

            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<Auth>(new List<Auth>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

    }
}