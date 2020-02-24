using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Data.Entity;
using Cinema.Api.Filters;
using Cinema.Api.Models;
using Cinema.Api.Models.MoviesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/MoviesType")]
    public class MoviesTypeController : ApiController
    {
        private readonly IMoviesTypeService _moviesTypeService;

        public MoviesTypeController()
        {
            _moviesTypeService = new MoviesTypeService();
        }
        [Route("GetAll")]
        [HttpPost]
        public ApiResponseModel<List<MoviesType>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Data.Entity.MoviesType>>();
            try
            {
                responseModel.Data = _moviesTypeService.GetAll();

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
        [TokenAuthorizeFilter(AuthCodeStatic.MOVIESTYPE_LIST)]
        public ApiResponseModel<PaginatedList<MoviesType>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            if (requestModel.Filter == null)        // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<MoviesType>>();
            try
            {
                var searchFilter = new Business.Models.MoviesType.MoviesTypeSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                responseModel.Data = _moviesTypeService.GetAllPaginatedBySearchFilter(searchFilter);
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
        [TokenAuthorizeFilter(AuthCodeStatic.MOVIESTYPE_EDIT)]
        public ApiResponseModel<MoviesType> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<MoviesType>();
            try
            {
                responseModel.Data = _moviesTypeService.GetById(requestModel.Id);
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
        [TokenAuthorizeFilter(AuthCodeStatic.MOVIESTYPE_ADD)]
        public ApiResponseModel<MoviesType> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<MoviesType>();
            try
            {
                var record = new MoviesType();
                record.Name = requestModel.Name;
                record.IsDeleted = false;
                var dbResult = _moviesTypeService.Add(record);
                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Saved";
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
        [TokenAuthorizeFilter(AuthCodeStatic.MOVIESTYPE_EDIT)]
        public ApiResponseModel<MoviesType> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<MoviesType>();
            try
            {
                var record = _moviesTypeService.GetById(requestModel.Id);
                record.Name = requestModel.Name;
                var dbResult = _moviesTypeService.Update(record);
                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Edited.";
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
        [TokenAuthorizeFilter(AuthCodeStatic.MOVIESTYPE_DELETE)]
        public ApiResponseModel<MoviesType> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<MoviesType>();
            try
            {
                var record = _moviesTypeService.GetById(requestModel.Id);
                var dbResult = _moviesTypeService.Delete(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // 'isDeleted= true' yapılan -> entity bilgisi geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage ="Success";
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
