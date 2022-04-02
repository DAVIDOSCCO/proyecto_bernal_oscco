using App.Ventas.WebApi.App_Start;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(App.Ventas.WebApi.Startup))]

namespace App.Ventas.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            RouteConfig.RegisterRoutes(config);
            DIConfig.ConfigureInjector(config);
            TokenConfig.ConfigureOAuth(app, config);
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
