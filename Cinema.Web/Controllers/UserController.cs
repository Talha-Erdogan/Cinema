﻿using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.User;
using Cinema.Web.Filters;
using Cinema.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IAuthService _authService;

        public UserController()
        {
            _userService = new UserService();
            _profileService = new ProfileService();
            _authService = new AuthService();
        }

        public ActionResult Login()
        {
            Models.User.LoginViewModel model = new LoginViewModel();
            //select lists
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Models.User.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            SessionLoginResult result = SessionHelper.Login(model.Username, model.Password, _userService, _authService, _profileService);
            if (result.IsSuccess)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // select lists
                return View(model);
            }
            else
            {
                ViewBag.Error = result.Message;
                // select lists
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Models.User.LogoutViewModel model = new LogoutViewModel();
            SessionHelper.Logout(_userService);
            return RedirectToAction(nameof(UserController.Login));
        }
        public ActionResult NotAuthorized()
        {
            return View();
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_LIST)]
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
            //select list
            model.ProfileSelectList = GetProfileSelectList(SessionHelper.CurrentUser.UserToken);

            UserSearchFilter searchFilter = new UserSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _userService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter); 
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.ProfileSelectList = GetProfileSelectList("");
                model.DataList = new Business.Models.PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_LIST)]
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

            model.ProfileSelectList = GetProfileSelectList("");

            UserSearchFilter searchFilter = new UserSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_Surname = model.Filter.Filter_Surname;
            searchFilter.Filter_ProfileId = model.Filter.Filter_ProfileId;

            var apiResponseModel = _userService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.ProfileSelectList = GetProfileSelectList("");
                model.DataList = new Business.Models.PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_ADD)]
        public ActionResult Add()
        {
            Models.User.AddViewModel model = new AddViewModel();
            //select lists
            model.ProfileSelectList = GetProfileSelectList("");
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_ADD)]
        [HttpPost]
        public ActionResult Add(Models.User.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                model.ProfileSelectList = GetProfileSelectList("");
                return View(model);
            }
            Business.Models.User.User user = new Business.Models.User.User();
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Mail = model.Mail;
            user.ProfileId = model.ProfileId;
            var apiResponseModel = _userService.Add(SessionHelper.CurrentUser.UserToken, user);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(UserController.List));
            }
            else
            {
                model.ProfileSelectList = GetProfileSelectList("");
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.User.AddViewModel model = new AddViewModel();
            //select lists
            model.ProfileSelectList = GetProfileSelectList("");
            var apiResponseModel = _userService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                model.ProfileSelectList = GetProfileSelectList("");
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var user = apiResponseModel.Data;

            if (user == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = user.Id;
            model.UserName  = user.UserName ;
            model.Password  = user.Password ;
            model.Name      = user.Name     ;
            model.Surname   = user.Surname  ;
            model.Mail      = user.Mail     ;
            model.ProfileId = user.ProfileId;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.User.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                model.ProfileSelectList = GetProfileSelectList("");
                return View(model);
            }

            var apiResponseModel = _userService.GetById(SessionHelper.CurrentUser.UserToken, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                model.ProfileSelectList = GetProfileSelectList("");
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var user = apiResponseModel.Data;

            if (user == null)
            {
                return View("_ErrorNotExist");
            }
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Mail = model.Mail;
            user.ProfileId = model.ProfileId;
            var apiEditResponseModel = _userService.Edit(SessionHelper.CurrentUser.UserToken, user);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                model.ProfileSelectList = GetProfileSelectList("");

                return View(model);
            }
            model.ProfileSelectList = GetProfileSelectList("");
            return RedirectToAction(nameof(UserController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.USER_DELETE)]
        public ActionResult Delete(int id)
        {
            Models.User.AddViewModel model = new AddViewModel();
            //select lists

            var apiResponseModel = _userService.GetById(SessionHelper.CurrentUser.UserToken, id); 
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(UserController.List));
            }
            var user = apiResponseModel.Data;

            if (user == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(UserController.List));
            }
            var apiDeleteResponseModel = _userService.Delete(SessionHelper.CurrentUser.UserToken, id);
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(UserController.List));
            }

            return RedirectToAction(nameof(UserController.List));
        }

        [NonAction]
        private List<SelectListItem> GetProfileSelectList(string userToken)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _profileService.GetAll(userToken);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }


    }
}