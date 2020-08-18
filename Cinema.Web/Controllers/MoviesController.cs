using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.Movies;
using Cinema.Web.Filters;
using Cinema.Web.Models.Movies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ISeanceService _seanceService;
        private readonly ISalonService _salonService;
        private readonly IMoviesTypeService _moviesTypeService;

        public MoviesController()
        {
            _moviesService = new MoviesService();
            _seanceService = new SeanceService();
            _salonService = new SalonService();
            _moviesTypeService = new MoviesTypeService();
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_LIST)]
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
            model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
            model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);

            MoviesSearchFilter searchFilter = new MoviesSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _moviesService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<MoviesWithDetail>(new List<MoviesWithDetail>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }
            // select lists

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_LIST)]
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
            //select list
            model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
            model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);

            MoviesSearchFilter searchFilter = new MoviesSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_SeanceId = model.Filter.Filter_SeanceId;
            searchFilter.Filter_SalonId = model.Filter.Filter_SalonId;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _moviesService.GetAllPaginatedWithDetailBySearchFilter(SessionHelper.CurrentUser.UserToken, searchFilter);
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<MoviesWithDetail>(new List<MoviesWithDetail>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_ADD)]
        public ActionResult Add()
        {
            Models.Movies.AddViewModel model = new AddViewModel();
            //select lists
            model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
            model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
            model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Movies.AddViewModel model, HttpPostedFileBase banner)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);
                return View(model);
            }
            WebImage img = new WebImage(banner.InputStream);
            FileInfo fotoinfo = new FileInfo(banner.FileName);
            string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
            img.Resize(150, 150);
            img.Save("~/Uploads/MoviesBanner/" + newfoto);

            Business.Models.Movies.Movies movies = new Business.Models.Movies.Movies();
            movies.SeanceId = model.SeanceId;
            movies.SalonId = model.SalonId;
            movies.Name = model.Name;
            movies.TypeId = model.TypeId;
            movies.Director = model.Director;
            movies.Banner = "/Uploads/MoviesBanner/" + newfoto;
            var apiResponseModel = _moviesService.Add(SessionHelper.CurrentUser.UserToken, movies);

            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(MoviesController.List));
            }
            else
            {
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);

                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Movies.AddViewModel model = new AddViewModel();
            var apiResponseModel = _moviesService.GetById(SessionHelper.CurrentUser.UserToken, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);

                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var movies = apiResponseModel.Data;

            if (movies == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = movies.Id;
            model.SeanceId = movies.SeanceId;
            model.SalonId = movies.SalonId;
            model.Name = movies.Name;
            model.TypeId = movies.TypeId;
            model.Director = movies.Director;
            model.Banner = movies.Banner;

            //select lists
            model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
            model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
            model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);

            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Movies.AddViewModel model, HttpPostedFileBase Banner)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);
                return View(model);
            }

            var apiResponseModel = _moviesService.GetById(SessionHelper.CurrentUser.UserToken, model.Id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);

                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var movies = apiResponseModel.Data;

            if (movies == null)
            {
                return View("_ErrorNotExist");
            }

            WebImage img = new WebImage(Banner.InputStream);
            FileInfo fotoinfo = new FileInfo(Banner.FileName);
            string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
            img.Resize(150, 150);
            img.Save("~/Uploads/MoviesBanner/" + newfoto);

            movies.SeanceId = model.SeanceId;
            movies.SalonId = model.SalonId;
            movies.Name = model.Name;
            movies.TypeId = model.TypeId;
            movies.Director = model.Director;
            //movies.Banner = model.Banner;
            movies.Banner = "/Uploads/MoviesBanner/" + newfoto;
            var apiEditResponseModel = _moviesService.Edit(SessionHelper.CurrentUser.UserToken, movies);
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                //select lists
                model.SeanceSelectList = GetSeanceSelectList(SessionHelper.CurrentUser.UserToken);
                model.SalonSelectList = GetSalonSelectList(SessionHelper.CurrentUser.UserToken);
                model.MoviesTypeSelectList = GetMoviesTypenSelectList(SessionHelper.CurrentUser.UserToken);
                return View(model);
            }

            return RedirectToAction(nameof(MoviesController.List));
        }

        [AppAuthorizeFilter(AuthCodeStatic.MOVIES_DELETE)]
        public ActionResult Delete(int id)
        {
            Models.Movies.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _moviesService.GetById(SessionHelper.CurrentUser.UserToken, id);
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(MoviesController.List));
            }
            var movies = apiResponseModel.Data;

            if (movies == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(MoviesController.List));
            }
            var apiDeleteResponseModel = _moviesService.Delete(SessionHelper.CurrentUser.UserToken, id);
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(MoviesController.List));
            }

            return RedirectToAction(nameof(MoviesController.List));
        }



        [NonAction]
        private List<SelectListItem> GetSeanceSelectList(string userToken)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _seanceService.GetAll(userToken);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetSalonSelectList(string userToken)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _salonService.GetAll(userToken);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetMoviesTypenSelectList(string userToken)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _moviesTypeService.GetAll(userToken);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }


    }
}