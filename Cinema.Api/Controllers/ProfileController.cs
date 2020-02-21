using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Data.Entity;
using Cinema.Api.Models;
using Cinema.Api.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;

        public ProfileController()
        {
            _profileService = new ProfileService();
        }

        [Route("GetAll")]
        [HttpPost]
        public ApiResponseModel<List<Profile>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Profile>>();
            try
            {
                responseModel.Data = _profileService.GetAll();

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
        public ApiResponseModel<PaginatedList<Profile>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            if (requestModel.Filter == null)        // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<Profile>>();
            try
            {
                var searchFilter = new Business.Models.Profile.ProfileSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                responseModel.Data = _profileService.GetAllPaginatedBySearchFilter(searchFilter);
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
        public ApiResponseModel<Profile> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Profile>();
            try
            {
                responseModel.Data = _profileService.GetById(requestModel.Id);
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
        public ApiResponseModel<Profile> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Profile>();
            try
            {
                var record = new Profile();
                record.Name = requestModel.Name;
                record.IsDeleted = false;
                var dbResult = _profileService.Add(record);
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
        public ApiResponseModel<Profile> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Profile>();
            try
            {
                var record = _profileService.GetById(requestModel.Id);
                record.Name = requestModel.Name;
                var dbResult = _profileService.Update(record);
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
        public ApiResponseModel<Profile> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Profile>();
            try
            {
                var record = _profileService.GetById(requestModel.Id);
                var dbResult = _profileService.Delete(record);

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
