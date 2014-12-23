using System.Web.Mvc;
using System.Web.Routing;

namespace Log4Grid.Management.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Users",
                url: "Users",
                defaults: new {controller = "Home", action = "Users", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "CreateUser",
                url: "CreateUser",
                defaults: new {controller = "Home", action = "CreateUser", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new {controller = "Home", action = "Login", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}