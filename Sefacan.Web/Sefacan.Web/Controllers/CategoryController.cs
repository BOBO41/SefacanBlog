using Sefacan.Framework.Controllers;
using Sefacan.Framework.Paging;
using Sefacan.Service;
using Sefacan.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Web.Controllers
{
    public class CategoryController : BaseController
    {
        #region Fields
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        private readonly IUrlService urlService;
        private readonly ISettingService settingService;
        #endregion

        #region Ctor
        public CategoryController(ICategoryService _categoryService,
            IPostService _postService,
            IUrlService _urlService,
            ISettingService _settingService)
        {
            categoryService = _categoryService;
            postService = _postService;
            urlService = _urlService;
            settingService = _settingService;
        }
        #endregion

        #region Methods
        public ActionResult Index(int? Id, int page = 1)
        {
            if (!Id.HasValue)
                return HomePage();

            var category = categoryService.GetById(Id.Value);

            if (category == null)
                return NotFound();

            if (!category.IsActive || category.IsDelete)
                return NotFound();

            int pageSize = settingService.GetSetting("general.pagesize").IntValue;

            var model = new CategoryDetailModel
            {
                Name = category.Name,
                Posts = postService.GetActives(Id.Value).Select(x => new PostModel
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
                }).ToPagedList(page - 1, pageSize)
            };

            return View(model);
        }
        #endregion
    }
}