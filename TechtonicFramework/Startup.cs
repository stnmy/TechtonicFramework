using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TechtonicFramework.Data;
using TechtonicFramework.Models.Users;

[assembly: OwinStartup(typeof(TechtonicFramework.Startup))]

namespace TechtonicFramework
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // OWIN-based Identity setup
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromDays(30),
                SlidingExpiration = true
            });

            // Web API setup
            var config = new HttpConfiguration();

            // CORS (adjust origin as needed)
            var cors = new EnableCorsAttribute("https://localhost:5000", "*", "*")
            {
                SupportsCredentials = true
            };
            config.EnableCors(cors);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
