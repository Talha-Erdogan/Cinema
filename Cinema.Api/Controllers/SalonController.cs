using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Data.Entity;
using Cinema.Api.Filters;
using Cinema.Api.Models;
using Cinema.Api.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/Salon")]
    public class SalonController : ApiController
    {
        private readonly ISalonService _salonService;

        public SalonController()
        {
            _salonService = new SalonService();
        }

        [Route("GetAll")]
        [HttpPost]
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_LIST)]
        public ApiResponseModel<List<Salon>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Salon>>();
            try
            {
                responseModel.Data = _salonService.GetAll();

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
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_LIST)]
        public ApiResponseModel<PaginatedList<Salon>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            if (requestModel.Filter == null)        // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<Salon>>();
            try
            {
                var searchFilter = new Business.Models.Salon.SalonSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                responseModel.Data = _salonService.GetAllPaginatedBySearchFilter(searchFilter);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_EDIT)]
        public ApiResponseModel<Salon> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Salon>();
            try
            {
                responseModel.Data = _salonService.GetById(requestModel.Id);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_ADD)]
        public ApiResponseModel<Salon> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Salon>();
            try
            {
                var record = new Salon();
                record.Name = requestModel.Name;
                record.IsDeleted = false;
                var dbResult = _salonService.Add(record);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_EDIT)]
        public ApiResponseModel<Salon> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Salon>();
            try
            {
                var record = _salonService.GetById(requestModel.Id);
                record.Name = requestModel.Name;
                var dbResult = _salonService.Update(record);
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
        [TokenAuthorizeFilter(AuthCodeStatic.SALON_DELETE)]
        public ApiResponseModel<Salon> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Salon>();
            try
            {
                var record = _salonService.GetById(requestModel.Id);
                var dbResult = _salonService.Delete(record);

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
