using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Deoendency resolver.
	/// </summary>
	public class DependencyResolver : IDependencyResolver
	{
		private bool isDisposed;

		/// <summary>
		/// The container
		/// </summary>
		protected IUnityContainer Container;


		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolver" /> class.
		/// </summary>
		/// <param name="container">The container.</param>
		protected DependencyResolver(IUnityContainer container)
		{
			Contract.Requires(container != null);

			this.Container = container;

			Contract.Ensures(this.Container != null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolver"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public DependencyResolver(IDependencyContainer container)
		{
			Contract.Requires(container != null);

			this.Container = container.Container;

			Contract.Ensures(this.Container != null);
		}


		/// <summary>
		/// Retrieves a service from the scope.
		/// </summary>
		/// <param name="serviceType">The service to be retrieved.</param>
		/// <returns>
		/// The retrieved service.
		/// </returns>
		public object GetService(Type serviceType)
		{
			Contract.Requires(this.Container != null);

			try
			{
				// Resolve given service type
				return this.Container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				// Dont throw exception if service
				// couldn't be resolved.
				return null;
			}
		}

		/// <summary>
		/// Retrieves a collection of services from the scope.
		/// </summary>
		/// <param name="serviceType">The collection of services to be retrieved.</param>
		/// <returns>
		/// The retrieved collection of services.
		/// </returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			Contract.Requires(this.Container != null);

			try
			{
				// Resolve given service type
				return this.Container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				// Dont throw exception if service
				// couldn't be resolved.
				return null;
			}
		}

		/// <summary>
		/// Starts a resolution scope.
		/// </summary>
		/// <returns>
		/// The dependency scope.
		/// </returns>
		public IDependencyScope BeginScope()
		{
			Contract.Requires(this.Container != null);

			// Create and return new instance of dependency resolver 
			// with child container instance.
			return new DependencyResolver(this.Container.CreateChildContainer());
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="DependencyResolver"/> class.
		/// </summary>
		~DependencyResolver()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (this.isDisposed)
				return;

			if (disposing)
			{
				// Dispose container
				if (this.Container != null)
				{
					this.Container.Dispose();
					this.Container = null;
				}
			}

			this.isDisposed = true;
		}
	}
}
