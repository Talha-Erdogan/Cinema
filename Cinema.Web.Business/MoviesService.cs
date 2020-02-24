using Cinema.Web.Business.Common;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
    public class MoviesService : IMoviesService
    {
        public ApiResponseModel<PaginatedList<MoviesWithDetail>> GetAllPaginatedWithDetailBySearchFilter(string userToken, MoviesSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<MoviesWithDetail>> result = new ApiResponseModel<PaginatedList<MoviesWithDetail>>()
            {
                Data = new PaginatedList<MoviesWithDetail>(new List<MoviesWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
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
                listFilter.Filter_SeanceId = searchFilter.Filter_SeanceId;
                listFilter.Filter_SalonId = searchFilter.Filter_SalonId;
                portalApiRequestModel.Filter = listFilter;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<MoviesWithDetail>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Movies>> GetAll(string userToken)
        {
            ApiResponseModel<List<Movies>> result = new ApiResponseModel<List<Movies>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Movies>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Movies> GetById(string userToken, int id)
        {
            ApiResponseModel<Movies> result = new ApiResponseModel<Movies>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Movies>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Movies> Add(string userToken, Movies movies)
        {
            ApiResponseModel<Movies> result = new ApiResponseModel<Movies>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.SeanceId = movies.SeanceId;
                portalApiRequestModel.SalonId = movies.SalonId;
                portalApiRequestModel.Name = movies.Name;
                portalApiRequestModel.TypeId = movies.TypeId;
                portalApiRequestModel.Director = movies.Director;
                portalApiRequestModel.Banner = movies.Banner;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Movies>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Movies> Edit(string userToken, Movies movies)
        {
            ApiResponseModel<Movies> result = new ApiResponseModel<Movies>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = movies.Id;
                portalApiRequestModel.SeanceId = movies.SeanceId;
                portalApiRequestModel.SalonId = movies.SalonId;
                portalApiRequestModel.Name = movies.Name;
                portalApiRequestModel.TypeId = movies.TypeId;
                portalApiRequestModel.Director = movies.Director;
                portalApiRequestModel.Banner = movies.Banner;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Movies>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Movies> Delete(string userToken, int moviesId)
        {
            ApiResponseModel<Movies> result = new ApiResponseModel<Movies>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = moviesId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Movies/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Movies>>().Result;
            }
            return result;
        }

    }
}
