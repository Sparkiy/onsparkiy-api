using System.Web.Http;
using onsparkiy.api.Common.DI;

namespace onsparkiy.api
{
	/// <summary>
	/// WebAPI configuration.
	/// </summary>
	public static class WebApiConfig
    {
		/// <summary>
		/// Registers the specified configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public static void Register(HttpConfiguration config)
        {
			// Register dependency injection
			RegisterDI(config);

            // Web API configuration and services

            // Web API routes
			RegisterRoutes(config);
        }

		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="config">The configuration.</param>
		private static void RegisterRoutes(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}

		/// <summary>
		/// Registers the dependency injection with configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		private static void RegisterDI(HttpConfiguration config)
	    {
			// Configure DI container
			var container = new DependencyContainer();

			// Assign dependency resolver to configuration
			config.DependencyResolver = new DependencyResolver(container);
		}
    }
}
