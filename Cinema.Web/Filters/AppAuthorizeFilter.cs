using Cinema.Web.Business.Common.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cinema.Web.Filters
{
    public class AppAuthorizeFilter : ActionFilterAttribute
    {
        private readonly string[] authCodeList;
        public AppAuthorizeFilter(params string[] authCodeList)
        {
            this.authCodeList = authCodeList;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerInfo = filterContext.ActionDescriptor as ActionDescriptor;

            if (filterContext != null)
            {
                string controllerName = controllerInfo.ControllerDescriptor.ControllerName;
                if (!SessionHelper.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
                }
                else if (SessionHelper.IsAuthenticated &&
                        !(SessionHelper.CurrentUser.UserAuthList != null
                        && SessionHelper.CurrentUser.UserAuthList.Count > 0
                        && SessionHelper.CurrentUser.UserAuthList.Where(r => authCodeList.Contains(r.Code)).Any())
                        )
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "NotAuthorized" }));
                }
            }
        }
    }
}