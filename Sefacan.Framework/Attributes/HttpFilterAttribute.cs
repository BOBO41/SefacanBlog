using Sefacan.Core.Infrastructure;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using System;
using System.Web.Mvc;

namespace Sefacan.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class HttpFilterAttribute : ActionFilterAttribute
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

            if (!WebHelper.IsSecureConnection)
            {
                var settingService = Engine.Resolve<ISettingService>();
                bool useWWW = settingService.GetSetting("general.use.www").BoolValue;
                if (useWWW)
                {
                    string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;

                    if (!url.StartsWith("http://www."))
                        url = url.Replace("http://", "http://www.");

                    filterContext.Result = new RedirectResult(url, true);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}