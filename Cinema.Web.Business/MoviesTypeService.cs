using Cinema.Web.Business.Common;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.MoviesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
   public  class MoviesTypeService : IMoviesTypeService
    {
        public ApiResponseModel<PaginatedList<MoviesType>> GetAllPaginatedWithDetailBySearchFilter(string userToken, MoviesTypeSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<MoviesType>> result = new ApiResponseModel<PaginatedList<MoviesType>>()
            {
                Data = new PaginatedList<MoviesType>(new List<MoviesType>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
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
                portalApiRequestModel.Filter = listFilter;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format(" MoviesType/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<MoviesType>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<MoviesType>> GetAll(string userToken)
        {
            ApiResponseModel<List<MoviesType>> result = new ApiResponseModel<List<MoviesType>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("MoviesType/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<MoviesType>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<MoviesType> GetById(string userToken, int id)
        {
            ApiResponseModel<MoviesType> result = new ApiResponseModel<MoviesType>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("MoviesType/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<MoviesType>>().Result;
            }
            return result;
        }

        public ApiResponseModel<MoviesType> Add(string userToken, MoviesType moviesType)
        {
            ApiResponseModel<MoviesType> result = new ApiResponseModel<MoviesType>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Name = moviesType.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("MoviesType/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<MoviesType>>().Result;
            }
            return result;
        }

        public ApiResponseModel<MoviesType> Edit(string userToken, MoviesType moviesType)
        {
            ApiResponseModel<MoviesType> result = new ApiResponseModel<MoviesType>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = moviesType.Id;
                portalApiRequestModel.Name = moviesType.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("MoviesType/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<MoviesType>>().Result;
            }
            return result;
        }

        public ApiResponseModel<MoviesType> Delete(string userToken, int moviesTypeId)
        {
            ApiResponseModel<MoviesType> result = new ApiResponseModel<MoviesType>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = moviesTypeId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("MoviesType/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<MoviesType>>().Result;
            }
            return result;
        }



    }
}
