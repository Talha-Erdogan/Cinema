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


        public ApiResponseModel<PaginatedList<UserWithDetail>> GetAllPaginatedWithDetailBySearchFilter(string userToken, UserSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<UserWithDetail>> result = new ApiResponseModel<PaginatedList<UserWithDetail>>()
            {
                Data = new PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllPaginatedRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.CurrentPage = searchFilter.CurrentPage;
                portalApiRequestModel.PageSize = searchFilter.PageSize;
                portalApiRequestModel.SortOn = searchFilter.SortOn;
                portalApiRequestModel.SortDirection = searchFilter.SortDirection;
                ListFilterRequestModel listFilter = new ListFilterRequestModel();
                listFilter.Filter_Name = searchFilter.Filter_Name;
                listFilter.Filter_Surname = searchFilter.Filter_Surname;
                listFilter.Filter_ProfileId = searchFilter.Filter_ProfileId;
                portalApiRequestModel.Filter = listFilter;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<UserWithDetail>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<User>> GetAll(string userToken)
        {
            ApiResponseModel<List<User>> result = new ApiResponseModel<List<User>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<User>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<User> GetById(string userToken, int id)
        {
            ApiResponseModel<User> result = new ApiResponseModel<User>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<User>>().Result;
            }
            return result;
        }

        public ApiResponseModel<User> Add(string userToken, User user)
        {
            ApiResponseModel<User> result = new ApiResponseModel<User>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.UserName = user.UserName;
                portalApiRequestModel.Password = user.Password;
                portalApiRequestModel.Name = user.Name;
                portalApiRequestModel.Surname = user.Surname;
                portalApiRequestModel.Mail = user.Mail;
                portalApiRequestModel.ProfileId = user.ProfileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<User>>().Result;
            }
            return result;
        }

        public ApiResponseModel<User> Edit(string userToken, User user)
        {
            ApiResponseModel<User> result = new ApiResponseModel<User>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = user.Id;
                portalApiRequestModel.UserName = user.UserName;
                portalApiRequestModel.Password = user.Password;
                portalApiRequestModel.Name = user.Name;
                portalApiRequestModel.Surname = user.Surname;
                portalApiRequestModel.Mail = user.Mail;
                portalApiRequestModel.ProfileId = user.ProfileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<User>>().Result;
            }
            return result;
        }

        public ApiResponseModel<User> Delete(string userToken, int userId)
        {
            ApiResponseModel<User> result = new ApiResponseModel<User>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = userId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("User/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<User>>().Result;
            }
            return result;
        }




    }
}
