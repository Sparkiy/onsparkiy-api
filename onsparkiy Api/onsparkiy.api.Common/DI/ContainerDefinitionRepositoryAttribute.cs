using System;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute for Repository implementations.
	/// </summary>
	public class ContainerDefinitionRepositoryAttribute : ContainerDefinitionContainerControlledAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerDefinitionRepositoryAttribute"/> class.
		/// </summary>
		/// <param name="contract">The contract.</param>
		public ContainerDefinitionRepositoryAttribute(Type contract) : base(contract)
		{
		}
	}
}