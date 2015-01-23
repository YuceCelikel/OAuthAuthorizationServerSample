using System;
using System.Threading.Tasks;
using System.Web.Http;
using AuthorizationServer.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace AuthorizationServer
{
  public partial class Startup 
  {
      public void ConfigureAuth(IAppBuilder app)
      {
          // Setup Authorization Server
          var oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
          {
              AuthorizeEndpointPath = new PathString("/api/oauth/authorize"),
              TokenEndpointPath = new PathString("/api/oauth/token"),
              AccessTokenExpireTimeSpan = TimeSpan.FromHours(5),
              ApplicationCanDisplayErrors = true,
              AllowInsecureHttp = true,
              Provider = new MyOAuthAuthorizationServerProvider()
          };
          app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
          app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


          var config = new HttpConfiguration();
          config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}", new { controller = "Home" });
          app.UseWebApi(config);
      }
  }
}