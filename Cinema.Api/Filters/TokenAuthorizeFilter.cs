using Cinema.Api.Business;
using Cinema.Api.Business.Enums;
using Cinema.Api.Business.Interfaces;
using Cinema.Api.Data.Entity;
using Cinema.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Cinema.Api.Filters
{
    public class TokenAuthorizeFilter : ActionFilterAttribute
    {
        private readonly IUserTokenService _userTokenService;
        private readonly IAuthService _authService;

        private readonly string[] _authCodeList;

        public TokenAuthorizeFilter(params string[] authCodeList)
        {
            _authCodeList = authCodeList;
            _userTokenService = new UserTokenService();
            _authService = new AuthService();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var authToken = "";
            if (actionContext.Request.Headers.Contains("Authorization"))
            {
                authToken = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
            }

            if (string.IsNullOrEmpty(authToken))
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "Token paramater not found.";

                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, responseModel);

                return;
            }

            authToken = authToken.Replace("Bearer ", "");
            var existUserToken = _userTokenService.GetByTokenWhichIsValid(authToken);

            if (existUserToken == null)
            {
                BaseResponseModel responseModel = new BaseResponseModel();
                responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                responseModel.ResultStatusMessage = "User token not found.";

                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, responseModel);

                return;
            }

            // auth yetki kontolleri yapma
            List<Auth> userAuthList = _authService.GetAllByProfileId(existUserToken.ProfileId);

            if (_authCodeList != null && _authCodeList.Count() > 0)
            {
                if (userAuthList == null || userAuthList.Count == 0 || (!userAuthList.Where(r => _authCodeList.Contains(r.Code)).Any()))
                {
                    BaseResponseModel responseModel = new BaseResponseModel();
                    responseModel.ResultStatusCode = ResultStatusCodeStatic.Error;
                    responseModel.ResultStatusMessage = "User not authorized.";

                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, responseModel);

                    return;
                }
            }
        }

    }
}