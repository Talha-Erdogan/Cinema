using Cinema.Web.Business.Common;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
    public class ProfileService: IProfileService
    {
        public ApiResponseModel<PaginatedList<Profile>> GetAllPaginatedWithDetailBySearchFilter(string userToken, ProfileSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Profile>> result = new ApiResponseModel<PaginatedList<Profile>>()
            {
                Data = new PaginatedList<Profile>(new List<Profile>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
            };
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllPaginatedRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.CurrentPage = searchFilter.CurrentPage;
                portalApiRequestModel.PageSize = searchFilter.PageSize;
                portalApiRequestModel.SortOn = searchFilter.SortOn;
                portalApiRequestModel.SortDirection = searchFilter.SortDirection;
                ListFilterRequestModel listFilter = new ListFilterRequestModel();
                listFilter.Filter_Code = searchFilter.Filter_Code;
                listFilter.Filter_Name = searchFilter.Filter_Name;
                portalApiRequestModel.Filter = listFilter;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<Profile>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Profile>> GetAll(string userToken)
        {
            ApiResponseModel<List<Profile>> result = new ApiResponseModel<List<Profile>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Profile>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Profile> GetById(string userToken, int id)
        {
            ApiResponseModel<Profile> result = new ApiResponseModel<Profile>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Profile>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Profile> Add(string userToken, Profile profile)
        {
            ApiResponseModel<Profile> result = new ApiResponseModel<Profile>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Code = profile.Code;
                portalApiRequestModel.Name = profile.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Profile>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Profile> Edit(string userToken, Profile profile)
        {
            ApiResponseModel<Profile> result = new ApiResponseModel<Profile>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = profile.Id;
                portalApiRequestModel.Code = profile.Code;
                portalApiRequestModel.Name = profile.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Profile>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Profile> Delete(string userToken, int profileId)
        {
            ApiResponseModel<Profile> result = new ApiResponseModel<Profile>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = profileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Profile/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Profile>>().Result;
            }
            return result;
        }

    }
}
