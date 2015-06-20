using System;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Dependency container contract.
	/// </summary>
	public interface IDependencyContainer : IDisposable
	{
		/// <summary>
		/// Registers the specified type to the container using the definition.
		/// Type must have ContainerDefinitionAttribute.
		/// </summary>
		/// <typeparam name="T">Type to register.</typeparam>
		void RegisterWithDefinition<T>() where T : class;

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		IUnityContainer Container { get; }
	}
}