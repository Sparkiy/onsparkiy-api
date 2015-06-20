using System;
using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;

namespace onsparkiy.api.Common.DI
{
	/// <summary>
	/// Container definition attribute contract.
	/// </summary>
	public interface IContainerDefinitionAttrubute : _Attribute
	{
		/// <summary>
		/// Gets the contract.
		/// </summary>
		/// <value>
		/// The contract.
		/// </value>
		Type Contract { get; }

		/// <summary>
		/// Gets or sets the class lifetime lifetime.
		/// </summary>
		/// <value>
		/// The lifetime.
		/// </value>
		LifetimeManager Lifetime { get; }
	}
}