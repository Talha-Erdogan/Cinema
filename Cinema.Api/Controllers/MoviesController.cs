using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.Movies;
using Cinema.Api.Models;
using Cinema.Api.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/Movies")]
    public class MoviesController : ApiController
    {
        private readonly IMoviesService _moviesService;

        public MoviesController()
        {
            _moviesService = new MoviesService();
        }

        [Route("GetAll")]
        [HttpPost]
        public ApiResponseModel<List<MoviesWithDetail>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<MoviesWithDetail>>();
            try
            {
                responseModel.Data = _moviesService.GetAllWithDetail();

                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage = "Success";
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }

            return responseModel;
        }

        [Route("GetAllPaginatedWithDetail")]
        [HttpPost]
        public ApiResponseModel<PaginatedList<MoviesWithDetail>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (requestModel.Filter == null)
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<MoviesWithDetail>>();

            try
            {
                var searchFilter = new Business.Models.Movies.MoviesSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;

                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                searchFilter.Filter_SalonId = requestModel.Filter.Filter_SalonId;
                searchFilter.Filter_SeanceId = requestModel.Filter.Filter_SeanceId;

                responseModel.Data = _moviesService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);

                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage = "Success";
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }

            return responseModel;
        }

        [Route("GetById")]
        [HttpPost]
        public ApiResponseModel<Data.Entity.Movies> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.Movies>();
            try
            {
                responseModel.Data = _moviesService.GetById(requestModel.Id);
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage = "Success";
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }
            return responseModel;
        }

        [Route("Add")]
        [HttpPost]
        public ApiResponseModel<Data.Entity.Movies> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.Movies>();
            try
            {
                var record = new Data.Entity.Movies();
                record.SeanceId = requestModel.SeanceId;
                record.SalonId = requestModel.SalonId;
                record.Name = requestModel.Name;
                record.TypeId = requestModel.TypeId;
                record.Director = requestModel.Director;
                record.Banner = requestModel.Banner;
                record.IsDeleted = false;

                var dbResult = _moviesService.Add(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "CouldNotBeSaved";
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }

            return responseModel;
        }

        [Route("Edit")]
        [HttpPost]
        public ApiResponseModel<Data.Entity.Movies> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.Movies>();
            try
            {
                var record = _moviesService.GetById(requestModel.Id);
                record.SeanceId = requestModel.SeanceId;
                record.SalonId = requestModel.SalonId;
                record.Name = requestModel.Name;
                record.TypeId = requestModel.TypeId;
                record.Director = requestModel.Director;
                record.Banner = requestModel.Banner;
                
                var dbResult = _moviesService.Update(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage =  "Could Not Be Edited";
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }

            return responseModel;
        }

        [Route("Delete")]
        [HttpPost]
        public ApiResponseModel<Data.Entity.Movies> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.Movies>();
            try
            {
                var record = _moviesService.GetById(requestModel.Id);
                var dbResult = _moviesService.Delete(record);
                if (dbResult > 0)
                {
                    responseModel.Data = record; // 'isDeleted= true' yapılan -> entity bilgisi geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Deleted";
                }
            }
            catch (Exception ex)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = ex.Message;
            }
            return responseModel;
        }
    }
}
