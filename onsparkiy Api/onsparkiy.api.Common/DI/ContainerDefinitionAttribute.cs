using System;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public abstract class ContainerDefinitionAttribute : Attribute, IContainerDefinitionAttrubute
	{
		/// <summary>
		/// Gets the contract.
		/// </summary>
		/// <value>
		/// The contract.
		/// </value>
		public Type Contract { get; }

		/// <summary>
		/// Gets or sets the class lifetime lifetime.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		public abstract LifetimeManager Lifetime { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerDefinitionAttribute"/> class.
		/// </summary>
		/// <param name="contract">The contract.</param>
		protected ContainerDefinitionAttribute(Type contract)
		{
			this.Contract = contract;
		}
	}
}