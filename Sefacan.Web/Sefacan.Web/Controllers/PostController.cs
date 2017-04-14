using Sefacan.Core.Entities;
using Sefacan.Core.Helpers;
using Sefacan.Framework;
using Sefacan.Framework.Controllers;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using Sefacan.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Web.Controllers
{
    public class PostController : BaseController
    {
        #region Fields
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        private readonly IUrlService urlService;
        private readonly ITagService tagService;
        private readonly ISettingService settingService;
        #endregion

        #region Ctor
        public PostController(ICategoryService _categoryService,
            IPostService _postService,
            IUrlService _urlService,
            ITagService _tagService,
            ISettingService _settingService)
        {
            categoryService = _categoryService;
            postService = _postService;
            urlService = _urlService;
            tagService = _tagService;
            settingService = _settingService;
        }
        #endregion

        #region Methods
        public ActionResult Index(int? Id)
        {
            if (!Id.HasValue)
                return HomePage();

            var post = postService.GetById(Id.Value);

            if (post == null)
                return NotFound();

            if (!post.IsActive || post.IsDelete)
                return NotFound();

            string cookieKey = string.Format(CookieConstant.POST_VIEW, post.Id);
            if (!CookieHelper.Exists(cookieKey))
            {
                CookieHelper.Set(cookieKey, WebHelper.IpAddress, 1);
                post.ViewCount += 1;
                postService.UpdatePost(post);
            }

            var model = new PostDetailModel
            {
                Id = post.Id,
                Title = post.Title,
                FullContent = post.FullContent,
                PicturePath = post.PicturePath,
                Url = urlService.GetUrl(post.Id, Core.Enums.EntityType.Post),
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                Description = post.Description,
                ViewCount = post.ViewCount,
                CategoryName = categoryService.GetById(post.CategoryId).Name,
                CategoryUrl = urlService.GetUrl(post.CategoryId, Core.Enums.EntityType.Category),
                CommentCount = postService.GetCommentCount(post.Id),
                AllowComment = post.AllowComment,
                Tags = tagService.GetTags(postId: post.Id).Select(x => new TagModel
                {
                    Name = x.TagName
                }).ToList()
            };

            if (settingService.GetSetting("post.related.view").BoolValue)
            {
                model.RelatedPosts = postService.GetRelatedPosts(post.CategoryId, post.Id).Select(x => new PostModel
                {
                    Title = x.Title,
                    PicturePath = x.PicturePath,
                    ShortContent = x.ShortContent,
                    CreateDate = x.CreateDate,
                    CategoryName = categoryService.GetById(x.CategoryId).Name,
                    CommentCount = 0,
                    ViewCount = x.ViewCount,
                    Url = urlService.GetUrl(x.Id, Core.Enums.EntityType.Post)
                }).ToList();
            }

            if (settingService.GetSetting("post.comment.enabled").BoolValue)
            {
                model.Comments = postService.GetComments(post.Id).Select(x => new CommentListModel
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    FullName = x.FullName,
                    Content = x.Content,
                    CreateDate = x.CreateDate.ToRelativeFormat(),
                    ChildComments = postService.GetChildComments(post.Id, x.Id).Select(c => new CommentListModel
                    {
                        Id = c.Id,
                        ParentId = c.ParentId,
                        FullName = c.FullName,
                        Content = c.Content,
                        CreateDate = c.CreateDate.ToRelativeFormat(),
                    }).ToList()
                }).ToList();
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Comment(FormCollection form)
        {
            if (!settingService.GetSetting("post.comment.enabled").BoolValue)
                return Json(new { errorMessage = "Yorum sistemi devredışı." });

            if (!string.IsNullOrEmpty(form["postId"]) &&
                !string.IsNullOrEmpty(form["name"]) &&
                !string.IsNullOrEmpty(form["email"]) &&
                !string.IsNullOrEmpty(form["message"]))
            {
                var comment = new Comment
                {
                    PostId = form["postId"].ToInt(),
                    ParentId = form["parentId"].ToInt(),
                    FullName = form["name"],
                    EmailAddress = form["email"],
                    Content = form["message"],
                    IsAccept = false,
                    CreateDate = DateTime.Now
                };
                postService.InsertComment(comment);

                var model = new CommentListModel
                {
                    Id = comment.Id,
                    ParentId = comment.ParentId,
                    FullName = comment.FullName,
                    Content = comment.Content,
                    CreateDate = comment.CreateDate.ToRelativeFormat()
                };
                return Json(model);
            }

            return Json(new { errorMessage = "Bir hata oluştu." });
        }
        #endregion
    }
}