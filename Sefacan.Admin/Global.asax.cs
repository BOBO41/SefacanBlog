using Sefacan.Core.Infrastructure;
using Sefacan.Framework.Helpers;
using Sefacan.Framework.Infrastructure;
using Sefacan.Service;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sefacan.Admin
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyRegister.Register();

            //Web Engine Optimization
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(ViewEngine.Instance);
            MvcHandler.DisableMvcResponseHeader = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        protected void Application_End()
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            WebHelper.ClearSession();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (WebHelper.IsLocal)
                return;

            if (WebHelper.IsStaticResource(Request))
                return;

            var exception = Server.GetLastError();
            Response.Clear();
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            //Logging
            var logService = Engine.Resolve<ILogService>();
            logService.Error(WebHelper.GetCurrentPageUrl(true), WebHelper.GetUrlReferrer,
                WebHelper.IpAddress, exception.Message, exception);

            if (exception is HttpException)
            {
                var httpException = exception as HttpException;
                if (httpException.GetHttpCode() == 404)
                    Context.Response.Redirect("/NotFound.html", true);
                else
                    Context.Response.Redirect("/Error.html", true);
            }
        }
    }
}
