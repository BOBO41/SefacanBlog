using Sefacan.Core.Infrastructure;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sefacan.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            try
            {
                if (filterContext.IsChildAction)
                    return;

                string controllerName = filterContext.Controller.GetType().FullName;
                if (!controllerName.Contains("Admin"))
                    return;

                if (WebHelper.IsAuthenticated)
                    return;

                if (SkipAuthorization(filterContext))
                    return;

                string referrerUrl = WebHelper.GetCurrentPageUrl(false);
                if (referrerUrl != "/")
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Login", action = "Index", returnUrl = referrerUrl }));
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                }
            }
            catch (Exception ex)
            {
                var logService = Engine.Resolve<ILogService>();
                logService.Error(WebHelper.GetCurrentPageUrl(true), WebHelper.GetUrlReferrer,
                    WebHelper.IpAddress, ex.Message, ex);
            }
        }

        protected bool SkipAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                return false;

            var actions = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
            var controllers = filterContext.Controller.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), true);

            return actions.Length > 0 || controllers.Length > 0;
        }
    }
}