using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Auth;
using Cinema.Web.Filters;
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

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_LIST)]
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
            
            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_LIST)]
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

            var apiResponseModel = _authService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
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

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_ADD)]
        public ActionResult Add()
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            //select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            Business.Models.Auth.Auth auth = new Business.Models.Auth.Auth();
            auth.Code = model.Code;
            auth.Name = model.Name;
            var apiResponseModel = _authService.Add(SessionHelper.CurrentUser.UserToken, auth);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(AuthController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _authService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var auth = apiResponseModel.Data;

            if (auth == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = auth.Id;
            model.Code = auth.Code;
            model.Name = auth.Name;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            var apiResponseModel = _authService.GetById(SessionHelper.CurrentUser.UserToken, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var auth = apiResponseModel.Data;

            if (auth == null)
            {
                return View("_ErrorNotExist");
            }

            auth.Code = model.Code;
            auth.Name = model.Name;

            var apiEditResponseModel = _authService.Edit(SessionHelper.CurrentUser.UserToken, auth);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                // select lists
                return View(model);
            }

            return RedirectToAction(nameof(AuthController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.AUTH_DELETE)]
        public ActionResult Delete(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _authService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(AuthController.List));
            }
            var auth = apiResponseModel.Data;

            if (auth == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(AuthController.List));
            }
            var apiDeleteResponseModel = _authService.Delete(SessionHelper.CurrentUser.UserToken, id); 
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(AuthController.List));
            }

            return RedirectToAction(nameof(AuthController.List));
        }

    }
}