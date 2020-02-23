using Cinema.Web.Business.Common;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
    public class SalonService : ISalonService
    {
        public ApiResponseModel<PaginatedList<Salon>> GetAllPaginatedWithDetailBySearchFilter(string userToken, SalonSearchFilter searchFilter)
        {
            ApiResponseModel<PaginatedList<Salon>> result = new ApiResponseModel<PaginatedList<Salon>>()
            {
                Data = new PaginatedList<Salon>(new List<Salon>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection)
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
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format(" Salon/GetAllPaginatedWithDetail"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<PaginatedList<Salon>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<List<Salon>> GetAll(string userToken)
        {
            ApiResponseModel<List<Salon>> result = new ApiResponseModel<List<Salon>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllRequestModel();
                portalApiRequestModel.UserToken = userToken;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Salon/GetAll"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Salon>>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Salon> GetById(string userToken, int id)
        {
            ApiResponseModel<Salon> result = new ApiResponseModel<Salon>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetByIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = id;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Salon/GetById"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Salon>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Salon> Add(string userToken, Salon salon)
        {
            ApiResponseModel<Salon> result = new ApiResponseModel<Salon>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Name = salon.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Salon/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Salon>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Salon> Edit(string userToken, Salon salon)
        {
            ApiResponseModel<Salon> result = new ApiResponseModel<Salon>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = salon.Id;
                portalApiRequestModel.Name = salon.Name;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Salon/Edit"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Salon>>().Result;
            }
            return result;
        }

        public ApiResponseModel<Salon> Delete(string userToken, int salonId)
        {
            ApiResponseModel<Salon> result = new ApiResponseModel<Salon>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.Id = salonId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("Salon/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<Salon>>().Result;
            }
            return result;
        }
    }
}
