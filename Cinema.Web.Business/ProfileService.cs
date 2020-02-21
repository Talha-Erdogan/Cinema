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

       
    }
}
