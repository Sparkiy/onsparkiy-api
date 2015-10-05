using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
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
			//RegisterDI(config);

			// Json formatting
			RegisterJson(config);

			// Web API routes
			RegisterRoutes(config);
		}

		/// <summary>
		/// Registers the json serialization and deserialization.
		/// </summary>
		/// <param name="config">The configuration.</param>
		private static void RegisterJson(HttpConfiguration config)
		{
			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
			config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
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
				routeTemplate: "{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
			config.Routes.MapHttpRoute(
				name: "ActionApi",
				routeTemplate: "{controller}/{action}/{id}",
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
