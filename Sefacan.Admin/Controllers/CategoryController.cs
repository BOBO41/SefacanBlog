using Sefacan.Admin.Models;
using Sefacan.Core.Entities;
using Sefacan.Core.Enums;
using Sefacan.Framework.Controllers;
using Sefacan.Service;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        #region Fields
        private readonly ICategoryService categoryService;
        private readonly IUrlService urlService;
        #endregion

        #region Ctor
        public CategoryController(ICategoryService _categoryService,
            IUrlService _urlService)
        {
            categoryService = _categoryService;
            urlService = _urlService;
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    IsActive = model.IsActive,
                };
                categoryService.InsertCategory(category);

                var urlRecord = new UrlRecord
                {
                    EntityId = category.Id,
                    UniqueUrl = model.Url,
                    EntityType = EntityType.Category
                };
                urlService.InsertUrl(urlRecord);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return RedirectToAction("Index");

            var category = categoryService.GetById(Id.Value);
            if (category == null)
                return NotFound();

            var model = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                IsActive = category.IsActive,
                Url = urlService.GetUrl(category.Id, EntityType.Category)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = categoryService.GetById(model.Id);
                category.Name = model.Name;
                category.IsActive = model.IsActive;
                categoryService.UpdateCategory(category);

                var urlRecord = urlService.GetByEntity(category.Id, EntityType.Category);
                if (urlRecord == null)
                {
                    urlRecord = new UrlRecord
                    {
                        EntityId = category.Id,
                        UniqueUrl = model.Url,
                        EntityType = EntityType.Category
                    };
                    urlService.InsertUrl(urlRecord);
                }
                else
                {
                    urlRecord.UniqueUrl = model.Url;
                    urlService.UpdateUrl(urlRecord);
                }

                return RedirectToAction("Edit", new { Id = model.Id });
            }

            ViewBag.Categories = categoryService.GetActives();
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Categories()
        {
            var model = categoryService.GetCategories().Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).ToList();

            return PartialView("_Categories", model);
        }
        #endregion
    }
}