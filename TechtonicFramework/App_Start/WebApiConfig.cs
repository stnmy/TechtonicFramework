using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.AspNet.WebApi;
using Newtonsoft.Json.Serialization;
using TechtonicFramework.Repository;
using TechtonicFramework.Filters;

namespace TechtonicFramework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Only JSON responses
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Global exception handling
            config.Filters.Add(new GlobalExceptionFilter());

            //// ------------------ Dependency Injection ------------------
            //var container = new UnityContainer();

            //// Repositories ONLY — do NOT register ApplicationDbContext here!
            ////container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<ICartRepository, CartRepository>(new HierarchicalLifetimeManager());

            //config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
