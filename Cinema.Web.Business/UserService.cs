using Cinema.Web.Business.Common;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
    public class UserService : IUserService
    {
        public ApiResponseModel<LoginResponseModel> Login(string username, string password)
        {
            ApiResponseModel<LoginResponseModel> responseModel = new ApiResponseModel<LoginResponseModel>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var portalApiRequestModel = new LoginRequestModel();
                portalApiRequestModel.Password = password;
                portalApiRequestModel.Username = username;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/Login"), portalApiRequestModel).Result;
                responseModel = response.Content.ReadAsAsync<ApiResponseModel<LoginResponseModel>>().Result;
            }
            return responseModel;
        }

        public ApiResponseModel<int> Logout()
        {
            ApiResponseModel<int> responseModel = new ApiResponseModel<int>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.CurrentUser.UserToken);
                var portalApiRequestModel = new LogoutRequestModel();
                portalApiRequestModel.UserToken = SessionHelper.CurrentUser.UserToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/Logout"), portalApiRequestModel).Result;
                responseModel = response.Content.ReadAsAsync<ApiResponseModel<int>>().Result;
            }
            return responseModel;
        }

    }
}
