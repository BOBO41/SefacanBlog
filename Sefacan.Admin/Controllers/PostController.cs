using Sefacan.Admin.Models;
using Sefacan.Core.Entities;
using Sefacan.Core.Enums;
using Sefacan.Core.Helpers;
using Sefacan.Framework.Controllers;
using Sefacan.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    public class PostController : BaseController
    {
        #region Fields
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IUrlService urlService;
        #endregion

        #region Ctor
        public PostController(IPostService _postService,
            ICategoryService _categoryService,
            IUrlService _urlService)
        {
            postService = _postService;
            categoryService = _categoryService;
            urlService = _urlService;
        }
        #endregion

        #region Methods
        public ActionResult Index()
        {
            var model = postService.GetPosts().Select(x => new PostGridModel
            {
                Id = x.Id,
                Title = x.Title,
                CategoryName = categoryService.GetById(x.CategoryId).Name,
                ViewCount = x.ViewCount,
                IsActive = x.IsActive,
                UpdateDate = x.UpdateDate,
                CreateDate = x.CreateDate
            }).OrderByDescending(x => x.CreateDate).ToList();

            return View(model);
        }

        #region Create
        public ActionResult Create()
        {
            ViewBag.Categories = categoryService.GetActives();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    PicturePath = model.PicturePath,
                    ShortContent = model.ShortContent,
                    FullContent = model.FullContent,
                    IsActive = model.IsActive,
                    AllowComment = model.AllowComment,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };
                postService.InsertPost(post);

                var urlRecord = new UrlRecord
                {
                    EntityId = post.Id,
                    UniqueUrl = model.Url,
                    EntityType = EntityType.Post
                };
                urlService.InsertUrl(urlRecord);

                return RedirectToAction("Edit", new { Id = post.Id });
            }

            ViewBag.Categories = categoryService.GetActives();
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return RedirectToAction("Index");

            var post = postService.GetById(Id.Value);
            if (post == null)
                return NotFound();

            var model = new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                CategoryId = post.CategoryId,
                Description = post.Description,
                PicturePath = post.PicturePath,
                ViewCount = post.ViewCount,
                ShortContent = post.ShortContent,
                FullContent = post.FullContent,
                IsActive = post.IsActive,
                IsDelete = post.IsDelete,
                AllowComment = post.AllowComment,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Url = urlService.GetUrl(Id.Value, EntityType.Post)
            };
            ViewBag.Categories = categoryService.GetActives();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = postService.GetById(model.Id);
                post.Title = model.Title;
                post.CategoryId = model.CategoryId;
                post.Description = model.Description;
                post.PicturePath = model.PicturePath;
                post.ShortContent = model.ShortContent;
                post.FullContent = model.FullContent;
                post.IsActive = model.IsActive;
                post.AllowComment = model.AllowComment;
                post.UpdateDate = DateTime.Now;
                postService.UpdatePost(post);

                var urlRecord = urlService.GetByEntity(post.Id, EntityType.Post);
                if (urlRecord == null)
                {
                    urlRecord = new UrlRecord
                    {
                        EntityId = post.Id,
                        UniqueUrl = model.Url,
                        EntityType = EntityType.Post
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
        #endregion

        [HttpPost]
        public void Delete(int Id)
        {
            var post = postService.GetById(Id);

            if (post != null)
                postService.DeletePost(post);
        }
        #endregion
    }
}