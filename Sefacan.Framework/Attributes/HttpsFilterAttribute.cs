using Sefacan.Core.Infrastructure;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using System;
using System.Web.Mvc;

namespace Sefacan.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class HttpsFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (filterContext.IsChildAction)
                return;

            string controllerName = filterContext.Controller.GetType().FullName;
            if (controllerName.Contains("Admin"))
                return;

            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            if (filterContext.HttpContext.Request.IsLocal)
                return;

            var settingService = Engine.Resolve<ISettingService>();
            bool SSLEnabled = settingService.GetSetting("general.use.ssl").BoolValue;
            if (!WebHelper.IsSecureConnection)
            {
                if (SSLEnabled)
                {
                    string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;

                    if (!url.StartsWith("https://www."))
                    {
                        url = url.Replace("https://", "https://www.");
                        filterContext.Result = new RedirectResult(url, true);
                    }
                }
            }
            else
            {
                //if SSL is not active on setting and current connection is secure, rediret to http://
                if (!SSLEnabled)
                {
                    string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;

                    if (!url.StartsWith("http://www."))
                    {
                        url = url.Replace("http://", "http://www.");
                        filterContext.Result = new RedirectResult(url, true);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
