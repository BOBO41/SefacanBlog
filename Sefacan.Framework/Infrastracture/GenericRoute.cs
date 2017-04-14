using Sefacan.Core.Infrastructure;
using Sefacan.Service;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Sefacan.Framework.Infrastructure
{
    public class GenericRoute : Route
    {
        private readonly string[] oldRoutes = { "OldCategory", "OldPost" };
        public GenericRoute(string url, IRouteHandler routeHandler)
               : base(url, routeHandler)
        {
        }

        public GenericRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public GenericRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public GenericRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        /// <summary>
        /// Yönlendirme sırasında tetiklenir ve o anki yönlendirme bilgilerini verir
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData routeData = base.GetRouteData(httpContext);

            if (routeData == null)
                return base.GetRouteData(httpContext);

            string routeName = routeData.DataTokens["RouteName"] as string;
            if (!string.IsNullOrEmpty(routeName) && oldRoutes.Contains(routeName))
            {
                routeName = routeName.Trim();

                var response = httpContext.Response;
                response.Status = "301 Moved Permanently";
                response.StatusCode = 301;
                response.StatusDescription = "New Url Mechanism";

                switch (routeName)
                {
                    case "OldPost":
                        {
                            string title = routeData.Values["title"] as string;
                            response.RedirectLocation = "/" + title;
                            response.End();
                            return routeData;
                        }
                    case "OldCategory":
                        {
                            string categoryName = routeData.Values["categoryName"] as string;
                            response.RedirectLocation = "/" + categoryName;
                            response.End();
                            return routeData;
                        }
                }
            }

            if (routeData.Values["SeName"] != null)
            {
                var url = routeData.Values["SeName"] as string;
                var urlRecord = Engine.Resolve<IUrlService>().GetUrl(url);

                if (urlRecord != null)
                {
                    switch (urlRecord.EntityType)
                    {
                        case Core.Enums.EntityType.Post:
                            {
                                routeData.Values["controller"] = "Post";
                                routeData.Values["action"] = "Index";
                                routeData.Values["Id"] = urlRecord.EntityId;
                                break;
                            }
                        case Core.Enums.EntityType.Category:
                            {
                                routeData.Values["controller"] = "Category";
                                routeData.Values["action"] = "Index";
                                routeData.Values["Id"] = urlRecord.EntityId;
                                break;
                            }
                        case Core.Enums.EntityType.Page:
                            break;
                        default:
                            break;
                    }
                }
            }

            return routeData;
        }
    }
}
