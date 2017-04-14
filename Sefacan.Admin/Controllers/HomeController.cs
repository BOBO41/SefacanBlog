using Sefacan.Framework.Controllers;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}