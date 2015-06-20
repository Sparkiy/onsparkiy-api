using System;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute for implementations that use ContianerControlled lifetime manager.
	/// </summary>
	public abstract class ContainerDefinitionContainerControlledAttribute : ContainerDefinitionAttribute
	{
		/// <summary>
		/// Gets or sets the class lifetime lifetime.
		/// This instance implements <see cref="ContainerControlledLifetimeManager"/>.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		public override LifetimeManager Lifetime => new ContainerControlledLifetimeManager();


		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerDefinitionContainerControlledAttribute"/> class.
		/// </summary>
		/// <param name="contract">The contract.</param>
		protected ContainerDefinitionContainerControlledAttribute(Type contract) : base(contract)
		{
		}
	}
}