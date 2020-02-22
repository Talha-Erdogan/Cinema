using Cinema.Web.Business;
using Cinema.Web.Business.Common.Session;
using Cinema.Web.Business.Enums;
using Cinema.Web.Business.Interfaces;
using Cinema.Web.Business.Models.ProfileDetail;
using Cinema.Web.Models.ProfileDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Web.Controllers
{
    public class ProfileDetailController : Controller
    {
        private readonly IProfileDetailService _profileDetailService;
        private readonly IProfileService _profileService;
        public ProfileDetailController()
        {
            _profileDetailService = new ProfileDetailService();
            _profileService = new ProfileService();
        }

        public ActionResult BatchEdit( int profileId = 0)
        {
            BatchEditViewModel model = new BatchEditViewModel();

            if (profileId > 0)
            {
                var profile = _profileService.GetById("", profileId).Data;//todo:token
                if (profile == null)
                {
                    return View("_ErrorNotExistProfile");
                }
                model.ProfileSelectList = GetProfileSelectList("");//todo:token

                model.AuthList = GetAllAuthByProfileId("", profileId);//todo:token
                model.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded("", profileId);//todo:token
            }
            else
            {
                model.ProfileSelectList = GetProfileSelectList("");//todo:token
                model.AuthList = new List<AuthCheckViewModel>();
                model.AuthWhichIsNotIncludeList = new List<AuthCheckViewModel>();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult BatchEdit(BatchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                BatchEditViewModel batchEditViewModel = new BatchEditViewModel();
                batchEditViewModel.ProfileSelectList = GetProfileSelectList("");//todo:token
                batchEditViewModel.AuthList = GetAllAuthByProfileId("",model.ProfileId);//todo:token
                batchEditViewModel.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded("", model.ProfileId);//todo:token
                return View(batchEditViewModel);
            }

            if (model.SubmitType == "Add")
            {
                if (model.AuthWhichIsNotIncludeList != null)
                {
                    List<AuthCheckViewModel> record = model.AuthWhichIsNotIncludeList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            ProfileDetail profileDetail = new ProfileDetail();
                            profileDetail.AuthId = item.Id;
                            profileDetail.ProfileId = model.ProfileId;
                            _profileDetailService.Add("", profileDetail);//todo:token
                        }
                    }
                }
            }
            if (model.SubmitType == "Delete")
            {
                if (model.AuthList != null)
                {
                    List<AuthCheckViewModel> record = model.AuthList.Where(x => x.Checked == true).ToList();
                    if (record != null)
                    {
                        foreach (var item in record)
                        {
                            var apiResponseModel = _profileDetailService.DeleteByProfileIdAndAuthId("", model.ProfileId, item.Id);//todo:token
                            if (apiResponseModel.ResultStatusCode == ResultStatusCodeStatic.Success)
                            {
                                //not error
                            }
                            else
                            {
                                BatchEditViewModel batchEditViewModel = new BatchEditViewModel();
                                batchEditViewModel.ProfileSelectList = GetProfileSelectList("");//todo:token
                                batchEditViewModel.AuthList = GetAllAuthByProfileId("",model.ProfileId);//todo:token
                                batchEditViewModel.AuthWhichIsNotIncludeList = GetAllAuthByProfileIdWhichIsNotIncluded("",  model.ProfileId);//todo:token
                                ViewBag.ErrorMessage = apiResponseModel.ResultStatusMessage;
                                return View(batchEditViewModel);
                            }
                        }
                    }
                }

            }

            return RedirectToAction(nameof(ProfileDetailController.BatchEdit), new {  profileId = model.ProfileId });
        }


        [NonAction]
        private List<SelectListItem> GetProfileSelectList(string userToken)
        {
            List<SelectListItem> resultList = new List<SelectListItem>();
            var apiResponseModel = _profileService.GetAll(userToken);
            resultList = apiResponseModel.Data.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList(); 
            return resultList;
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileId(string userToken,int profileId)
        {
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            var apiResponseModel = _profileDetailService.GetAllAuthByProfileId("", profileId);//todo:token
            resultList = apiResponseModel.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name = a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }

        [NonAction]
        private List<AuthCheckViewModel> GetAllAuthByProfileIdWhichIsNotIncluded(string userToken, int profileId)
        {
            List<AuthCheckViewModel> resultList = new List<AuthCheckViewModel>();
            var apiResponseModel = _profileDetailService.GetAllAuthByProfileIdWhichIsNotIncluded("", profileId);//todo:token
            resultList = apiResponseModel.Data.Select(a => new AuthCheckViewModel() { Id = a.Id, Name = a.Name, Checked = false, Code = a.Code }).ToList();
            return resultList;
        }
    }
}