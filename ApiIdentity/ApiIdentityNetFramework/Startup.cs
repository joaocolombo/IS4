using Owin;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Security.Cookies;

//[assembly: OwinStartup("Startup", typeof(ApiIdentityNetFramework.Startup))]

namespace ApiIdentityNetFramework
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app); configure IS4 here (you just need this)
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44332",
                RequiredScopes = new[] { "api1" }
            });
            //configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            //require authentication for all controllers

            config.Filters.Add(new AuthorizeAttribute());
            app.UseWebApi(config);
        }

    }
}
