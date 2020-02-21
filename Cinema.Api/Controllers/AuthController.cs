using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Data.Entity;
using Cinema.Api.Models;
using Cinema.Api.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;
        private readonly IUserTokenService _userTokenService;

        public AuthController()
        {
            _authService = new AuthService();
            _userTokenService = new UserTokenService();
        }


        [Route("GetAll")]
        [HttpPost]
        public ApiResponseModel<List<Auth>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Auth>>();
            try
            {
                responseModel.Data = _authService.GetAll();

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
        public ApiResponseModel<PaginatedList<Auth>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            if (requestModel.Filter == null)        // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<Auth>>();
            try
            {
                var searchFilter = new Business.Models.Auth.AuthSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;
                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                responseModel.Data = _authService.GetAllPaginatedBySearchFilter(searchFilter);
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

        [Route("GetAllByCurrentUser")]
        [HttpPost]
        public ApiResponseModel<List<Data.Entity.Auth>> GetAllByCurrentUser([FromBody]GetAllByCurrentUserRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<Data.Entity.Auth>>();

            try
            {
                // token bilgisinde ilgili user'ın profileid bilgisi elde edilir
                var userProfileId = _userTokenService.GetByToken(requestModel.UserToken).ProfileId;

                responseModel.Data = _authService.GetAllByProfileId(userProfileId);
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
        public ApiResponseModel<Auth> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Auth>();
            try
            {
                responseModel.Data = _authService.GetById(requestModel.Id);
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
        public ApiResponseModel<Auth> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Auth>();
            try
            {
                var record = new Auth();
                record.Name = requestModel.Name;
                record.IsDeleted = false;
                var dbResult = _authService.Add(record);
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
        public ApiResponseModel<Auth> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Auth>();
            try
            {
                var record = _authService.GetById(requestModel.Id);
                record.Name = requestModel.Name;
                var dbResult = _authService.Update(record);
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
        public ApiResponseModel<Auth> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<Auth>();
            try
            {
                var record = _authService.GetById(requestModel.Id);
                var dbResult = _authService.Delete(record);

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
