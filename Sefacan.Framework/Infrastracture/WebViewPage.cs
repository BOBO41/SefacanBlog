using Sefacan.Core.Cache;
using Sefacan.Core.Entities;
using Sefacan.Core.Infrastructure;
using Sefacan.Service;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Sefacan.Framework.Infrastructure
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private ISettingService settingService;
        private ICacheManager cacheManager;

        public HelperResult RenderSection(string sectionName, Func<object, HelperResult> defaultContent)
        {
            return IsSectionDefined(sectionName) ? RenderSection(sectionName) : defaultContent(new object());
        }

        public override void InitHelpers()
        {
            settingService = Engine.Resolve<ISettingService>();
            cacheManager = Engine.Resolve<ICacheManager>();
            base.InitHelpers();
        }
        
        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, string.Empty);

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }
                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }

        public delegate Setting Settings(string name);
        private Settings _setting;

        public Settings Setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = (key) =>
                    {
                        //return cacheManager.Get(string.Format(CacheConstant.SETTING_ITEM, key), () =>
                        //{
                            return settingService.GetSetting(key);
                        //});
                    };
                }
                return _setting;
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}