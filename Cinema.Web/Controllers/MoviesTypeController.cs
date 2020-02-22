using Cinema.Web.Business;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.MoviesType;
using Cinema.Web.Models.MoviesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class MoviesTypeController : Controller
    {
        private readonly IMoviesTypeService _moviesTypeService;
        public MoviesTypeController()
        {
            _moviesTypeService = new MoviesTypeService();
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
            MoviesTypeSearchFilter searchFilter = new MoviesTypeSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;

            var apiResponseModel = _moviesTypeService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter); //todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<MoviesType>(new List<MoviesType>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
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

            MoviesTypeSearchFilter searchFilter = new MoviesTypeSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
                        searchFilter.Filter_Name = model.Filter.Filter_Name;

            var apiResponseModel = _moviesTypeService.GetAllPaginatedWithDetailBySearchFilter("", searchFilter);//todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                model.DataList = apiResponseModel.Data;
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                model.DataList = new Business.Models.PaginatedList<MoviesType>(new List<MoviesType>(), 0, model.CurrentPage.Value, model.PageSize.Value, model.SortOn, model.SortDirection);
                return View(model);
            }

            // select lists
            return View(model);
        }

        public ActionResult Add()
        {
            Models.MoviesType.AddViewModel model = new AddViewModel();
            //select lists

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Models.MoviesType.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            Business.Models.MoviesType.MoviesType moviesType = new Business.Models.MoviesType.MoviesType();

            moviesType.Name = model.Name;
            var apiResponseModel = _moviesTypeService.Add("", moviesType);//todo:token
            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
            {
                return RedirectToAction(nameof(MoviesTypeController.List));
            }
            else
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage != null ? apiResponseModel.ResultStatusMessage : "Not Saved";
                return View(model);
            }
        }


        public ActionResult Edit(int id)
        {
            Models.MoviesType.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _moviesTypeService.GetById("", id); //todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }
            var moviesType = apiResponseModel.Data;

            if (moviesType == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = moviesType.Id;
            model.Name = moviesType.Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Models.MoviesType.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //select lists
                return View(model);
            }

            var apiResponseModel = _moviesTypeService.GetById("", model.Id);//todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                //select lists
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return View(model);
            }

            var moviesType = apiResponseModel.Data;

            if (moviesType == null)
            {
                return View("_ErrorNotExist");
            }

                        moviesType.Name = model.Name;

            var apiEditResponseModel = _moviesTypeService.Edit("", moviesType);//todo:token
            if (apiEditResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiEditResponseModel.ResultStatusMessage != null ? apiEditResponseModel.ResultStatusMessage : "Not Edited";
                // todo: select lists
                return View(model);
            }

            return RedirectToAction(nameof(MoviesTypeController.List));
        }

        public ActionResult Delete(int id)
        {
            Models.MoviesType.AddViewModel model = new AddViewModel();
            //select lists
            var apiResponseModel = _moviesTypeService.GetById("", id); //todo:token
            if (apiResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(MoviesTypeController.List));
            }
            var moviesType = apiResponseModel.Data;

            if (moviesType == null)
            {
                ViewBag.ErrorMessage = "Not Found Record";
                return RedirectToAction(nameof(MoviesTypeController.List));
            }
            var apiDeleteResponseModel = _moviesTypeService.Delete("", id); //todo:token
            if (apiDeleteResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                ViewBag.ErrorMessage = apiDeleteResponseModel.ResultStatusMessage;
                return RedirectToAction(nameof(MoviesTypeController.List));
            }

            return RedirectToAction(nameof(MoviesTypeController.List));
        }



    }
}