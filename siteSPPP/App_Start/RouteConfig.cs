using System.Web.Mvc;
using System.Web.Routing;

namespace siteSPPP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{num}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, num = UrlParameter.Optional }
            );
        }
    }
}
