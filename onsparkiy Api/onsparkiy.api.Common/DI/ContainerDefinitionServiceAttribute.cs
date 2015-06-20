using System;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute for Service implementations.
	/// </summary>
	public class ContainerDefinitionServiceAttribute : ContainerDefinitionPerResolveAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerDefinitionServiceAttribute"/> class.
		/// </summary>
		/// <param name="contract">The contract.</param>
		public ContainerDefinitionServiceAttribute(Type contract) : base(contract)
		{
		}
	}
}