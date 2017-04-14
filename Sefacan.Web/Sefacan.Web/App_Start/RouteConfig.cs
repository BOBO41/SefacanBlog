using Sefacan.Framework.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sefacan.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Sitemap",
                url: "sitemap.xml",
                defaults: new { controller = "Home", action = "Sitemap" },
                namespaces: new[] { "Sefacan.Web.Controllers" });

            routes.MapRoute(
                name: "About",
                url: "About-Me",
                defaults: new { controller = "Page", action = "About" },
                namespaces: new[] { "Sefacan.Web.Controllers" });

            routes.MapRoute(
                name: "Contact",
                url: "Contact",
                defaults: new { controller = "Page", action = "Contact" },
                namespaces: new[] { "Sefacan.Web.Controllers" });

            routes.MapRoute(
                name: "Project",
                url: "Projects",
                defaults: new { controller = "Page", action = "Project" },
                namespaces: new[] { "Sefacan.Web.Controllers" });

            //routes.MapRoute(
            //    name: "Tag",
            //    url: "Tag/{tag}",
            //    defaults: new { controller = "Page", action = "Tag" },
            //    namespaces: new[] { "Sefacan.Web.Controllers" });

            routes.MapRoute(
                name: "Search",
                url: "Search/{term}",
                defaults: new { controller = "Page", action = "Search" },
                namespaces: new[] { "Sefacan.Web.Controllers" });

            /* Old Category Url */
            routes.MapGenericPathRoute(
                name: "OldCategory",
                url: "kategori/{id}/{categoryName}.html",
                defaults: new { controller = "Home", action = "NotFound" },
                namespaces: new[] { "Sefacan.Web.Controllers" });
            /* Old Category Url */

            /* Old Post Url */
            routes.MapGenericPathRoute(
                name: "OldPost",
                url: "post/{id}/{title}.html",
                defaults: new { controller = "Home", action = "NotFound" },
                namespaces: new[] { "Sefacan.Web.Controllers" });
            /* Old Post Url */

            /* Generic Route for Categories & Posts */
            routes.MapGenericPathRoute(
                name: "UniqueUrl",
                url: "{SeName}",
                defaults: new { controller = "Home", action = "NotFound" },
                namespaces: new[] { "Sefacan.Web.Controllers" });
            /* Generic Route for Categories & Posts */

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Sefacan.Web.Controllers" });
        }
    }
}