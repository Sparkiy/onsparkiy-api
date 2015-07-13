using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace onsparkiy.api.Models.Contracts
{
	/// <summary>
	/// Identity user contract.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TLogin">The type of the login.</typeparam>
	/// <typeparam name="TRole">The type of the role.</typeparam>
	/// <typeparam name="TClaim">The type of the claim.</typeparam>
	public interface IIdentityUser<out TKey, TLogin, TRole, TClaim> : IUser<TKey> where TLogin : IdentityUserLogin<TKey> where TRole : IdentityUserRole<TKey> where TClaim : IdentityUserClaim<TKey>
	{
		/// <summary>
		/// Email
		/// 
		/// </summary>
		string Email { get; set; }

		/// <summary>
		/// True if the email is confirmed, default is false
		/// 
		/// </summary>
		bool EmailConfirmed { get; set; }

		/// <summary>
		/// The salted/hashed form of the user password
		/// 
		/// </summary>
		string PasswordHash { get; set; }

		/// <summary>
		/// A random value that should change whenever a users credentials have changed (password changed, login removed)
		/// 
		/// </summary>
		string SecurityStamp { get; set; }

		/// <summary>
		/// PhoneNumber for the user
		/// 
		/// </summary>
		string PhoneNumber { get; set; }

		/// <summary>
		/// True if the phone number is confirmed, default is false
		/// 
		/// </summary>
		bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		/// Is two factor enabled for the user
		/// 
		/// </summary>
		bool TwoFactorEnabled { get; set; }

		/// <summary>
		/// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
		/// 
		/// </summary>
		DateTime? LockoutEndDateUtc { get; set; }

		/// <summary>
		/// Is lockout enabled for this user
		/// 
		/// </summary>
		bool LockoutEnabled { get; set; }

		/// <summary>
		/// Used to record failures for the purposes of lockout
		/// 
		/// </summary>
		int AccessFailedCount { get; set; }

		/// <summary>
		/// Navigation property for user roles
		/// 
		/// </summary>
		ICollection<TRole> Roles { get; }

		/// <summary>
		/// Navigation property for user claims
		/// 
		/// </summary>
		ICollection<TClaim> Claims { get; }

		/// <summary>
		/// Navigation property for user logins
		/// 
		/// </summary>
		ICollection<TLogin> Logins { get; }
	}
}