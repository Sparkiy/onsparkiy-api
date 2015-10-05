using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using onsparkiy.api.DAL.Contexts;
using onsparkiy.api.DAL.Repositories;
using Owin;

[assembly: OwinStartup(typeof(onsparkiy.api.Startup))]
namespace onsparkiy.api
{
	/// <summary>
	/// Bootstraps the Owin application
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Configurations the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			// Configure OAuth
			this.ConfigureOAuth(app);

			// Configure WebAPI and Enable CORS with onsparkiy
			WebApiConfig.Register(config);
			app.UseWebApi(config);

			// Enable auto-migrations
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<SparkiyDbContext, onsparkiy.api.DAL.Migrations.Configuration>());
		}

		/// <summary>
		/// Configures the OAuth.
		/// </summary>
		/// <param name="app">The application.</param>
		public void ConfigureOAuth(IAppBuilder app)
		{
			var oauthServerOptions = new OAuthAuthorizationServerOptions()
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new SimpleAuthorizationServerProvider()
			};

			// Token Generation
			app.UseOAuthAuthorizationServer(oauthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}