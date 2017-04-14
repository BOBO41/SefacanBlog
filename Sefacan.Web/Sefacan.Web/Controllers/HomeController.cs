using Sefacan.Core.Cache;
using Sefacan.Core.Helpers;
using Sefacan.Core.Infrastructure;
using Sefacan.Framework;
using Sefacan.Framework.Controllers;
using Sefacan.Framework.Helpers;
using Sefacan.Framework.Paging;
using Sefacan.Service;
using Sefacan.Web.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Sefacan.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        private readonly IUrlService urlService;
        private readonly ITagService tagService;
        private readonly ICacheManager cacheManager;
        private readonly ISettingService settingService;
        #endregion

        #region Ctor
        public HomeController(ICategoryService _categoryService,
            IPostService _postService,
            IUrlService _urlService,
            ITagService _tagService,
            ICacheManager _cacheManager,
            ISettingService _settingService)
        {
            categoryService = _categoryService;
            postService = _postService;
            urlService = _urlService;
            tagService = _tagService;
            cacheManager = _cacheManager;
            settingService = _settingService;
        }
        #endregion

        #region Methods
        public ActionResult Index(int page = 1)
        {
            int pageSize = settingService.GetSetting("general.pagesize").IntValue;
            var posts = postService.GetActives().Select(x => new PostModel
            {
                Title = x.Title,
                PicturePath = x.PicturePath,
                ShortContent = x.ShortContent,
                CreateDate = x.CreateDate,
                Url = urlService.GetUrl(x.Id, Core.Enums.EntityType.Post),
                CommentCount = postService.GetCommentCount(x.Id),
                CategoryName = categoryService.GetById(x.CategoryId).Name,
                CategoryUrl = urlService.GetUrl(x.CategoryId, Core.Enums.EntityType.Category),
                ViewCount = x.ViewCount
            }).ToPagedList(page - 1, pageSize);

            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var categories = cacheManager.Get(CacheConstant.ALL_CATEGORIES, () =>
            {
                return categoryService.GetActives().Select(x => new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PostCount = postService.GetCount(x.Id),
                    Url = urlService.GetUrl(x.Id, Core.Enums.EntityType.Category)
                }).ToList();
            });

            return PartialView("_Header", categories);
        }

        [ChildActionOnly]
        public ActionResult SideBar()
        {
            var model = new SidebarModel()
            {
                Categories = cacheManager.Get(CacheConstant.ALL_CATEGORIES, () =>
                {
                    return categoryService.GetActives().Select(x => new CategoryModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        PostCount = postService.GetCount(x.Id),
                        Url = urlService.GetUrl(x.Id, Core.Enums.EntityType.Category)
                    }).ToList();
                }),

                RecentPosts = cacheManager.Get(CacheConstant.RECENT_POSTS, () =>
                {
                    return postService.GetRecents().Select(x => new PostModel
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
                }),

                PopularPosts = cacheManager.Get(CacheConstant.POPULAR_POSTS, () =>
                {
                    return postService.GetPopulars().Select(x => new PostModel
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
                }),

                Tags = tagService.GetTags(10).Select(x => new TagModel
                {
                    Id = x.Id,
                    Name = x.TagName
                }).ToList()
            };

            if (settingService.GetSetting("post.comment.enabled").BoolValue &&
                settingService.GetSetting("post.recentcomment").BoolValue)
            {
                model.RecentComments = postService.GetRecentComments(5).Select(x => new CommentModel
                {
                    FullName = x.FullName,
                    PostName = postService.GetById(x.PostId).Title,
                    PostUrl = urlService.GetUrl(x.PostId, Core.Enums.EntityType.Post)
                }).ToList();
            }

            return PartialView("_Sidebar", model);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }

        public ActionResult Sitemap()
        {
            string domainUrl = WebHelper.GetDomainUrl + "/";
            var categories = categoryService.GetActives().Select(x => new SitemapItem
            {
                loc = domainUrl + urlService.GetUrl(x.Id, Core.Enums.EntityType.Category),
                changefreq = "weekly",
                lastmod = new DateTime(2015, 4, 5)
            }).ToList();

            var posts = postService.GetActives().Select(x => new SitemapItem
            {
                loc = domainUrl + urlService.GetUrl(x.Id, Core.Enums.EntityType.Post),
                changefreq = "weekly",
                lastmod = x.UpdateDate
            }).ToList();

            categories.AddRange(posts);
            var xml = SiteMap.Generate(categories);

            return Content(xml, MimeTypes.TextXml, Encoding.UTF8);
        }
        #endregion
    }
}