using System;
using System.Diagnostics.Contracts;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Dependency container.
	/// </summary>
	public class DependencyContainer : IDependencyContainer
	{
		private bool isDisposed;


		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyContainer"/> class.
		/// </summary>
		public DependencyContainer()
		{
			this.Container = new UnityContainer();

			Contract.Ensures(this.Container != null);
		}


		/// <summary>
		/// Registers the specified type to the container using the definition.
		/// Type must have ContainerDefinitionAttribute.
		/// </summary>
		/// <typeparam name="T">Type to register.</typeparam>
		public void RegisterWithDefinition<T>() where T : class
		{
			Contract.Requires(this.Container != null);

			this.Container.RegisterWithDefinition<T>();
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		public IUnityContainer Container { get; private set; }


		/// <summary>
		/// Finalizes an instance of the <see cref="DependencyContainer"/> class.
		/// </summary>
		~DependencyContainer()
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