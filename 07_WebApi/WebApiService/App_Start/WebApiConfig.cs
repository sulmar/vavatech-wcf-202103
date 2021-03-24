using Autofac;
using Autofac.Integration.WebApi;
using Bogus;
using FakeServices;
using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace WebApiService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            ContainerBuilder builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<FakeProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<ProductFaker>().As<Faker<Product>>().SingleInstance();
            
            IContainer container = builder.Build();

            // Install-Package Autofac.WebApi2
            // https://autofaccn.readthedocs.io/en/latest/integration/webapi.html
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
