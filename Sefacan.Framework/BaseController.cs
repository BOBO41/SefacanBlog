using Sefacan.Core.Cache;
using Sefacan.Core.Entities;
using Sefacan.Core.Helpers;
using Sefacan.Core.Infrastructure;
using Sefacan.Framework.Attributes;
using Sefacan.Framework.Helpers;
using Sefacan.Framework.Infrastructure;
using Sefacan.Service;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace Sefacan.Framework.Controllers
{
    [HttpFilter]
    [HttpsFilter]
    [AuthorizeFilter]
    public abstract class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        #region Redirect
        [AllowAnonymous]
        public virtual ActionResult Error()
        {
            return Redirect("/Error.html");
        }

        [AllowAnonymous]
        public virtual ActionResult NotFound()
        {
            return Redirect("/NotFound.html");
        }

        public virtual ActionResult HomePage()
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Show Message
        [NonAction]
        public void ShowSuccessMessage(string body)
        {
            ShowSuccessMessage("Success", body);
        }

        [NonAction]
        public void ShowSuccessMessage(string title, string body)
        {
            ShowMessage(title, body, "success");
        }

        [NonAction]
        public void ShowErrorMessage(string body)
        {
            ShowErrorMessage("Error", body);
        }

        [NonAction]
        public void ShowErrorMessage(string title, string body)
        {
            ShowMessage(title, body, "error");
        }

        [NonAction]
        public void ShowInfoMessage(string body)
        {
            ShowInfoMessage("Info", body);
        }

        [NonAction]
        public void ShowInfoMessage(string title, string body)
        {
            ShowMessage(title, body, "info");
        }

        [NonAction]
        private void ShowMessage(string title, string body, string type)
        {
            TempData["modalTitle"] = title;
            TempData["modalBody"] = body;
            TempData["messageType"] = type;
            TempData.Keep();
        }
        #endregion
        
        public ActionResult ClearCache()
        {
            Engine.Resolve<ICacheManager>().Clear();
            ShowSuccessMessage("Tüm önbellek temizlendi");

            string urlReferer = WebHelper.GetUrlReferrer;
            if (urlReferer.IsNullOrEmpty())
                return RedirectToAction("Index", "Home");
            else
                return Redirect(urlReferer);
        }
        
        public XmlResult Xml(XDocument xmlDocument)
        {
            return new XmlResult(xmlDocument);
        }

        public Setting GetSetting(string name)
        {
            var settingService = Engine.Resolve<ISettingService>();

            return settingService.GetSetting(name);
        }
    }
}