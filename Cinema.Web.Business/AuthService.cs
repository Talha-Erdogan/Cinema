using Cinema.Web.Business.Common;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Cinema.Web.Business.Interfaces;

namespace Cinema.Web.Business
{
    public class AuthService: IAuthService
    {

        public ApiResponseModel<PaginatedList<Auth>> GetAllPaginatedWithDetailBySearchFilter(string userToken, AuthSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Auth>> result = new ApiResponseModel<PaginatedList<Auth>>()
            {
                Data = new PaginatedList<Auth>(new List<Auth>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllPaginatedRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.CurrentPage = searchFilter.CurrentPage;
                portalApiRequestModel.PageSize = searchFilter.PageSize;
                portalApiRequestModel.SortOn = searchFilter.SortOn;
                portalApiRequestModel.SortDirection = searchFilter.SortDirection;

                ListFilterRequestModel listFilter = new ListFilterRequestModel();
                listFilter.Filter_Name = searchFilter.Filter_Name;
                listFilter.Filter_Code = searchFilter.Filter_Code;
                portalApiRequestModel.Filter = listFilter;

                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<Auth>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Auth>> GetAll(string userToken)
        {
            ApiResponseModel<List<Auth>> result = new ApiResponseModel<List<Auth>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Auth>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Auth>> GetAllByCurrentUser(string userToken)
        {
            ApiResponseModel<List<Auth>> result = new ApiResponseModel<List<Auth>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllByCurrentUserRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/GetAllByCurrentUser"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Auth>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> GetById(string userToken, int id)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Add(string userToken, Auth auth)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Code = auth.Code;
                portalApiRequestModel.Name = auth.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Edit(string userToken, Auth auth)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = auth.Id;
                portalApiRequestModel.Code = auth.Code;
                portalApiRequestModel.Name = auth.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Auth> Delete(string userToken, int authId)
        {
            ApiResponseModel<Auth> result = new ApiResponseModel<Auth>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = authId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Auth/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Auth>>().Result;
            }
            return result;
        }
    }
}
