using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Challenge3_a_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "NotBorrowed",
            routeTemplate: "api/Books/Borrowed",
            defaults: new { controller = "Books", action = "Borrowed", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "hkhkhkhk",
                routeTemplate: "api/Books/{id}",
                defaults: new { controller = "Books", action = "GetBooks", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
