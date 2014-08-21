using BaseApp.Api.App_Start;
using BaseApp.Api.Providers;
using BaseApp.Data;
using BaseApp.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(BaseApp.Api.Startup))]
namespace BaseApp.Api
{
    /// <summary>
    /// This class will be fired once server starts
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            IoCConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public void ConfigureOAuth(IAppBuilder app)
        {
            UserManagerFactory = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AppDbContext()));

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(UserManagerFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}