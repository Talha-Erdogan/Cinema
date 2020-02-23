using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Business.Models;
using Cinema.Api.Business.Models.User;
using Cinema.Api.Data.Entity;
using Cinema.Api.Models;
using Cinema.Api.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Api.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;

        public UserController()
        {
            _userService = new UserService();
            _userTokenService = new UserTokenService();
        }

        [Route("GetAll")]
        [HttpPost]
        public ApiResponseModel<List<UserWithDetail>> GetAll([FromBody]GetAllRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<List<UserWithDetail>>();
            try
            {
                responseModel.Data = _userService.GetAllWithDetail();

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
        public ApiResponseModel<PaginatedList<UserWithDetail>> GetAllPaginatedWithDetail([FromBody]GetAllPaginatedRequestModel requestModel)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (requestModel.Filter == null)
            {
                requestModel.Filter = new ListFilterRequestModel();
            }

            var responseModel = new ApiResponseModel<PaginatedList<UserWithDetail>>();

            try
            {
                var searchFilter = new Business.Models.User.UserSearchFilter();
                searchFilter.CurrentPage = requestModel.CurrentPage.HasValue ? requestModel.CurrentPage.Value : 1;
                searchFilter.PageSize = requestModel.PageSize.HasValue ? requestModel.PageSize.Value : 10;
                searchFilter.SortOn = requestModel.SortOn;
                searchFilter.SortDirection = requestModel.SortDirection;

                searchFilter.Filter_Name = requestModel.Filter.Filter_Name;
                searchFilter.Filter_Surname = requestModel.Filter.Filter_Surname;
                searchFilter.Filter_ProfileId = requestModel.Filter.Filter_ProfileId;

                responseModel.Data = _userService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);

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
        public ApiResponseModel<User> GetById([FromBody]GetByIdRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<User>();
            try
            {
                responseModel.Data = _userService.GetById(requestModel.Id);
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
        public ApiResponseModel<User> Add([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<User>();
            try
            {
                var record = new User();
                record.UserName = requestModel.UserName;
                record.Name = requestModel.Name;
                record.Password = requestModel.Password;
                record.Surname = requestModel.Surname;
                record.Mail = requestModel.Mail;
                record.ProfileId = requestModel.ProfileId;
                record.IsDeleted = false;
                var dbResult = _userService.Add(record);
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
        public ApiResponseModel<User> Edit([FromBody]AddRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<User>();
            try
            {
                var record = _userService.GetById(requestModel.Id);
                record.UserName = requestModel.UserName;
                record.Name = requestModel.Name;
                record.Password = requestModel.Password;
                record.Surname = requestModel.Surname;
                record.Mail = requestModel.Mail;
                record.ProfileId = requestModel.ProfileId;

                var dbResult = _userService.Update(record);

                if (dbResult > 0)
                {
                    responseModel.Data = record; // oluşturulan entity bilgisinde id kolonu atanmış olur ve entity geri gönderiliyor
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
                    responseModel.ResultStatusMessage = "Success";
                }
                else
                {
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "Could Not Be Edited";
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
        public ApiResponseModel<User> Delete([FromBody]DeleteRequestModel requestModel)
        {
            var responseModel = new ApiResponseModel<User>();
            try
            {
                var record = _userService.GetById(requestModel.Id);
                var dbResult = _userService.Delete(record);
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


        [Route("Login")]
        [HttpPost]
        public ApiResponseModel<LoginResponseModel> Login([FromBody]LoginRequestModel requestModel)
        {
            ApiResponseModel<LoginResponseModel> responseModel = new ApiResponseModel<LoginResponseModel>();
            var user = _userService.GetByUsernameAndPassword(requestModel.Username, requestModel.Password);
            if (user == null)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "User Not Be Found";
                return responseModel;
            }

            // portal api'de token üretilecek, token tablosuna kaydedilecek ve api'de dönülen kullanıcı bilgileri ve token bilgisi geri dönülecek 
            UserToken userToken = new UserToken();
            userToken.IsValid = true;
            userToken.ProfileId = user.ProfileId;
            userToken.Token = Guid.NewGuid().ToString();
            userToken.Username = user.UserName;
            userToken.ValidBeginDate = DateTime.Now;
            userToken.ValidEndDate = userToken.ValidBeginDate.AddHours(6); //6 saat gecerlilik verdik default olarak

            int resultAddToken = _userTokenService.Add(userToken);
            if (resultAddToken <= 0)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "User Token Could Not Be Saved";
                return responseModel;
            }
            LoginResponseModel loginResponse = new LoginResponseModel()
            {
                Id = user.Id,
                UserName = user.UserName,
               // Password = user.Password,//apiden password dönülmemeli
                Name = user.Name,
                Surname = user.Surname,
                Mail = user.Mail,
                ProfileId = user.ProfileId,
                IsDeleted = user.IsDeleted,
                UserToken = userToken.Token,
            };

            responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
            responseModel.ResultStatusMessage = "Success";
            responseModel.Data = loginResponse;
            return responseModel;

        }

        [Route("Logout")]
        [HttpPost]
        public ApiResponseModel<int> Logout([FromBody]LogoutRequestModel requestModel)
        {
            // token'ın geçerliliğinin sonlandırılması işlevidir.
            ApiResponseModel<int> responseModel = new ApiResponseModel<int>();

            // UserToken tablosundaki token'ın geçerliliği sonlandırılacak. 
            UserToken userToken = _userTokenService.GetByToken(requestModel.UserToken);
            userToken.IsValid = false;
            userToken.LogoutDateTime = DateTime.Now;
            int resultUpdateToken = _userTokenService.Update(userToken);
            if (resultUpdateToken <= 0)
            {
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "Failed To Terminate User Token";
                return responseModel;
            }
            responseModel.Data = resultUpdateToken;
            responseModel.ResultStatusCode = ResultStatusCodeStatic.Success;
            return responseModel;
        }
    }
}
