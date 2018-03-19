using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI.WebControls;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ApiIdentityNetFramework
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

    }
}
