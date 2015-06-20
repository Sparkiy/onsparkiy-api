using System;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute for implementations that use PerResolve lifetime manager.
	/// </summary>
	public abstract class ContainerDefinitionPerResolveAttribute : ContainerDefinitionAttribute
	{
		/// <summary>
		/// Gets or sets the class lifetime lifetime.
		/// This instance implements <see cref="PerResolveLifetimeManager"/>.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		public override LifetimeManager Lifetime => new PerResolveLifetimeManager();

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerDefinitionPerResolveAttribute"/> class.
		/// </summary>
		/// <param name="contract">The contract.</param>
		protected ContainerDefinitionPerResolveAttribute(Type contract) : base(contract)
		{
		}
	}
}