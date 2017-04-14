using Sefacan.Admin.Models;
using Sefacan.Core.Entities;
using Sefacan.Framework.Controllers;
using Sefacan.Service;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region Fields
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                userService.InsertUser(user);

                return RedirectToAction("Edit", new { Id = user.Id });
            }

            return View(model);
        }

        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return RedirectToAction("Index");

            var user = userService.GetById(Id.Value);
            if (user == null)
                return NotFound();

            var model = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetById(model.Id);

                user.UserName = model.UserName;
                user.Password = model.Password;
                userService.UpdateUser(user);

                return RedirectToAction("Edit", new { Id = user.Id });
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Users()
        {
            var users = userService.GetUsers().Select(x => new UserModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Password = x.Password
            }).ToList();

            return PartialView("_Users", users);
        }
        #endregion
    }
}