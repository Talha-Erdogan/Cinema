using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Models;
using Cinema.Api.Models.ProfileDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/ProfileDetail")] 
    public class ProfileDetailController : ApiController
    {
        private readonly IProfileDetailService _profileDetailService;

        public ProfileDetailController()
        {
            _profileDetailService = new ProfileDetailService();
        }

        [Route("GetAllAuth")]
        [HttpPost]
        public ApiResponseModel<List<Data.Entity.Auth>> GetAllAuthByProfileId([FromBody]GetAllAuthByProfileIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Data.Entity.Auth>>();

            if (requestModel.ProfileId <= 0)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "Profile Must Be Entered";
                return responseModel;
            }
            try
            {
                responseModel.Data = _profileDetailService.GetAllAuthByProfileId(requestModel.ProfileId);
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

        [Route("GetAllAuthWhichIsNotIncluded")]
        [HttpPost]
        public ApiResponseModel<List<Data.Entity.Auth>> GetAllAuthByProfileIdWhichIsNotIncluded([FromBody]GetAllAuthByProfileIdWhichIsNotIncludedRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Data.Entity.Auth>>();
            if (requestModel.ProfileId <= 0)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "Profile Must Be Entered";
                return responseModel;
            }

            try
            {
                responseModel.Data = _profileDetailService.GetAllAuthByProfileIdWhichIsNotIncluded(requestModel.ProfileId);

                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage =  "Success";
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
        public ApiResponseModel<Data.Entity.ProfileDetail> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.ProfileDetail>();
            try
            {
                responseModel.Data = _profileDetailService.GetById(requestModel.Id);

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


        [Route("GetByProfileIdAndAuthId")]
        [HttpPost]
        public ApiResponseModel<Data.Entity.ProfileDetail> GetByProfileIdAndAuthId([FromBody]GetByProfileIdAndAuthIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.ProfileDetail>();
            try
            {
                responseModel.Data = _profileDetailService.GetByProfileIdAndAuthId(requestModel.ProfileId, requestModel.AuthId);

                responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                responseModel.ResultStatusMessage =  "Success";
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
        public ApiResponseModel<Data.Entity.ProfileDetail> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.ProfileDetail>();
            try
            {
                var record = new Data.Entity.ProfileDetail();
                record.AuthId = requestModel.AuthId;
                record.ProfileId = requestModel.ProfileId;

                var dbResult = _profileDetailService.Add(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage ="Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage =  "Could Not Be Saved";
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
        public ApiResponseModel<Data.Entity.ProfileDetail> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Data.Entity.ProfileDetail>();
            try
            {
                var record = _profileDetailService.GetById(requestModel.Id);
                record.AuthId = requestModel.AuthId;
                record.ProfileId = requestModel.ProfileId;

                var dbResult = _profileDetailService.Update(record);

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

        [Route("Delete")]
        [HttpPost]
        public ApiResponseModel<int> DeleteByProfileIdAndAuthId([FromBody]DeleteByProfileIdAndAuthIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<int>();
            try
            {
                responseModel.Data = _profileDetailService.DeleteByProfileIdAndAuthId(requestModel.ProfileId, requestModel.AuthId);
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

    }
}
