using System;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using TechtonicFramework.Data;

namespace TechtonicFramework
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Disable EF initializer for production
            Database.SetInitializer<ApplicationDbContext>(null);

            // Optional seeding
            // DbSeeder.Seed();
        }
    }
}
