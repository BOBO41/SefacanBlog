using Sefacan.Admin.Models;
using Sefacan.Core.Helpers;
using Sefacan.Framework;
using Sefacan.Framework.Controllers;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        #region Fields
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public LoginController(IUserService _userService)
        {
            userService = _userService;
        }
        #endregion

        #region Methods
        public ActionResult Index(string url)
        {
            if (WebHelper.IsAuthenticated)
                return HomePage();

            ViewBag.returnUrl = url;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string url)
        {
            if (ModelState.IsValid)
            {
                var user = userService.CheckUser(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı!");
                    return View();
                }

                string restaurantCookie = user.ToJsonString().ToBase64();
                CookieHelper.Set(CookieConstant.CURRENT_USER, restaurantCookie, 1);

                if (string.IsNullOrEmpty(url))
                    return HomePage();

                return Redirect(url);
            }

            return View();
        }
        #endregion
    }
}