using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Data.Entity;
using Cinema.Api.Filters;
using Cinema.Api.Models;
using Cinema.Api.Models.Seance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/Seance")]
    public class SeanceController : ApiController
    {
        private readonly ISeanceService  _seanceService;

        public SeanceController()
        {
            _seanceService = new SeanceService();
        }

        [Route("GetAll")]
        [HttpPost]
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_LIST)]
        public ApiResponseModel<List<Seance>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Seance>>();
            try
            {
                responseModel.Data = _seanceService.GetAll();

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
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_LIST)]
        public ApiResponseModel<PaginatedList<Seance>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            if (requestModel.Filter == null)        // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<Seance>>();
            try
            {
                var searchFilter = new Business.Models.Seance.SeanceSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                responseModel.Data = _seanceService.GetAllPaginatedBySearchFilter(searchFilter);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_EDIT)]
        public ApiResponseModel<Seance> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Seance>();
            try
            {
                responseModel.Data = _seanceService.GetById(requestModel.Id);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_ADD)]
        public ApiResponseModel<Seance> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Seance>();
            try
            {
                var record = new Seance();
                record.Name = requestModel.Name;
                record.Date = requestModel.Date;
                record.Time = requestModel.Time;
                record.IsDeleted = false;
                var dbResult = _seanceService.Add(record);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_EDIT)]
        public ApiResponseModel<Seance> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Seance>();
            try
            {
                var record = _seanceService.GetById(requestModel.Id);
                record.Name = requestModel.Name;
                record.Date = requestModel.Date;
                record.Time = requestModel.Time;
                var dbResult = _seanceService.Update(record);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SEANCE_DELETE)]
        public ApiResponseModel<Seance> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Seance>();
            try
            {
                var record = _seanceService.GetById(requestModel.Id);
                var dbResult = _seanceService.Delete(record);

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
