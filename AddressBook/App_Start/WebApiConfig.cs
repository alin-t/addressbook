using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AddressBook.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "AddItem",
                routeTemplate: "add/{name}/{email}/{phone}",
                defaults: new { controller = "AddressBook", action = "add" }
            );

            config.Routes.MapHttpRoute(
                name: "DeleteItem",
                routeTemplate: "{action}/{name}",
                defaults: new { controller = "AddressBook", action = "delete" }
            );

            config.Routes.MapHttpRoute(
                name: "GetByName",
                routeTemplate: "{action}/{name}",
                defaults: new { controller = "AddressBook", action= "getbyname" }
            );

            config.Routes.MapHttpRoute(
                name: "GetAll",
                routeTemplate: "",
                defaults: new { controller = "AddressBook", action = "get" }
            );
        }
    }
}
