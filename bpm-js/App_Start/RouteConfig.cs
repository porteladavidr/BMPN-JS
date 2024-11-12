using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bpm_js
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Diagrama", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DiagramaCreate",
                url: "{controller}/Create/{id}",
                defaults: new { controller = "Diagrama", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}
