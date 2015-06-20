using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// <see cref="UnityContainer"/> extensions.
	/// </summary>
	public static class UnityContainerExtensions
	{
		/// <summary>
		/// Registers the specified type to the container using the definition.
		/// Type must have ContainerDefinitionAttribute.
		/// </summary>
		/// <typeparam name="T">Type to register.</typeparam>
		/// <param name="container">The container.</param>
		/// <exception cref="System.InvalidOperationException">Given type does not have ContainerDefinitionAttribute.</exception>
		public static void RegisterWithDefinition<T>(this IUnityContainer container)
			where T : class
		{
			Contract.Requires(container != null);

			// Retrieve destination type
			var destinationType = typeof(T);

			// Retrieve container definition
			// This is required, if container definition is not defines - throw exception
			var containerDefinition = destinationType.GetCustomAttribute<ContainerDefinitionAttribute>();
			if (containerDefinition == null)
				throw new InvalidOperationException("Given type does not have ContainerDefinitionAttribute.");

			// Register type
			container.RegisterType(containerDefinition.Contract, destinationType, containerDefinition.Lifetime);
		}
	}
}