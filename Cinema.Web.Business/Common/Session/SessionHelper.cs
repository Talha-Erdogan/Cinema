using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cinema.Web.Business.Common.Session
{
    public class SessionHelper
    {
        public static HttpContext CurrentHttpContext => HttpContext.Current;

        public static SessionUser CurrentUser
        {
            get
            {
                if (CurrentHttpContext.Session["Cinema_CurrentUser"] == null)
                {
                    return null;
                }
                else
                {
                    return (SessionUser)CurrentHttpContext.Session["Cinema_CurrentUser"];
                }
            }
            set
            {
                CurrentHttpContext.Session["Cinema_CurrentUser"] = value;
            }
        }
        public static bool IsAuthenticated
        {
            get
            {
                if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.UserToken) && !string.IsNullOrEmpty(CurrentUser.UserName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static SessionLoginResult Login(string userName, string password, IUserService userService, IAuthService authService, IProfileService profileService)
        {
            Models.User.LoginResponseModel existUser = null;
           
                var apiUserResponseModel = userService.Login(userName, password);
                if (apiUserResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success || apiUserResponseModel.Data == null || apiUserResponseModel == null)
                {
                    return new SessionLoginResult(false, "Username or password wrong."); 
                }
                existUser = apiUserResponseModel.Data;

            SessionUser currentUser = new SessionUser();
            currentUser.UserToken = existUser.UserToken;
            currentUser.UserName = existUser.UserName;
            currentUser.Name = existUser.Name;
            currentUser.Surname = existUser.Surname;
            currentUser.Mail = existUser.Mail;
            currentUser.ProfileId = existUser.ProfileId;
           
            var apiAuthResponseModel = authService.GetAllByCurrentUser(existUser.UserToken);
            if (apiAuthResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                return new SessionLoginResult(false, string.Format("Can not find auth info from Cinema system for user. {0} {1}", apiAuthResponseModel.ResultStatusCode,apiAuthResponseModel.ResultStatusMessage));
            }

            currentUser.UserAuthList = apiAuthResponseModel.Data;

            var apiProfileResponseModel = profileService.GetById(existUser.UserToken, existUser.ProfileId);
            if (apiProfileResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success)
            {
                return new SessionLoginResult(false, string.Format("Can not profile info from Cinema system for user. {0} {1}",apiProfileResponseModel.ResultStatusCode,apiProfileResponseModel.ResultStatusMessage));
            }
            currentUser.Profile = apiProfileResponseModel.Data;
            if (currentUser.Profile == null)
            {
                return new SessionLoginResult(false, "Can not find profile info from Cinema system for user.");
            }

            CurrentUser = currentUser;

            return new SessionLoginResult(true, null);
        }

        public static bool Logout(IUserService userService)
        {
            var apiUserResponseModel = userService.Logout();
            CurrentHttpContext.Session.Clear();
            foreach (var item in CurrentHttpContext.Session.Keys)
            {
                CurrentHttpContext.Session.Remove(item.ToString());
            }


            if (apiUserResponseModel.ResultStatusCode != ResultStatusCodeStatic.Success || apiUserResponseModel == null || apiUserResponseModel.Data <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string GetCurrentRequestIpAddress()
        {
            string requestIpAddress = "";
            try
            {
                if (!string.IsNullOrEmpty(CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                {
                    requestIpAddress = CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else if (!string.IsNullOrEmpty(CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"]))
                {
                    requestIpAddress = CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    requestIpAddress = CurrentHttpContext.Request.UserHostAddress;
                }
            }
            catch 
            {
            }
            return requestIpAddress;
        }

        public static bool CheckAuthForCurrentUser(params string[] authCodeList)
        {
            bool result = false;

            if (CurrentUser == null || CurrentUser.UserAuthList == null || CurrentUser.UserAuthList.Count == 0)
            {
                return result;
            }

            if (CurrentUser.UserAuthList.Where(r => authCodeList.Contains(r.Code)).Any())
            {
                result = true;
            }
            return result;
        }

    }
}
