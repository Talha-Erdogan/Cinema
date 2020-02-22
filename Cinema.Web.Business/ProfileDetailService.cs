using Cinema.Web.Business.Common;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models;
using Cinema.Web.Business.Models.ProfileDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business
{
    public class ProfileDetailService : IProfileDetailService
    {
        public ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileId(string userToken, int profileId)
        {
            ApiResponseModel<List<Business.Models.Auth.Auth>> result = new ApiResponseModel<List<Business.Models.Auth.Auth>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllAuthByProfileIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.ProfileId = profileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("ProfileDetail/GetAllAuth"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Business.Models.Auth.Auth>>>().Result;
            }
            return result;
        }
        public ApiResponseModel<List<Business.Models.Auth.Auth>> GetAllAuthByProfileIdWhichIsNotIncluded(string userToken, int profileId)
        {
            ApiResponseModel<List<Business.Models.Auth.Auth>> result = new ApiResponseModel<List<Business.Models.Auth.Auth>>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new GetAllAuthByProfileIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.ProfileId = profileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("ProfileDetail/GetAllAuthWhichIsNotIncluded"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<List<Business.Models.Auth.Auth>>>().Result;
            }
            return result;
        }
        public ApiResponseModel<ProfileDetail> Add(string userToken, ProfileDetail profileDetail)
        {
            ApiResponseModel<ProfileDetail> result = new ApiResponseModel<ProfileDetail>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new AddRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.AuthId = profileDetail.AuthId;
                portalApiRequestModel.ProfileId = profileDetail.ProfileId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("ProfileDetail/Add"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<ProfileDetail>>().Result;
            }
            return result;
        }
        public ApiResponseModel<int> DeleteByProfileIdAndAuthId(string userToken, int profileId, int authId)
        {
            ApiResponseModel<int> result = new ApiResponseModel<int>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(ConfigHelper.ApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var portalApiRequestModel = new DeleteByProfileIdAndAuthIdRequestModel();
                portalApiRequestModel.UserToken = userToken;
                portalApiRequestModel.ProfileId = profileId;
                portalApiRequestModel.AuthId = authId;
                HttpResponseMessage response = httpClient.PostAsJsonAsync(string.Format("ProfileDetail/Delete"), portalApiRequestModel).Result;
                result = response.Content.ReadAsAsync<ApiResponseModel<int>>().Result;
            }
            return result;
        }
    }
}
