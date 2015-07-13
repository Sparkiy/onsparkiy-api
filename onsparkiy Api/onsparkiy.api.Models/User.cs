using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using onsparkiy.api.Models.Contracts;
using IUser = onsparkiy.api.Models.Contracts.IUser;

namespace onsparkiy.api.Models
{
	/// <summary>
	/// User model.
	/// </summary>
	public class User : IdentityUser, IUser
	{
		/// <summary>
		/// Gets or sets the profile.
		/// </summary>
		/// <value>
		/// The profile.
		/// </value>
		public virtual UserProfile Profile { get; set; }


		/// <summary>
		/// Generates the user identity.
		/// </summary>
		/// <param name="manager">The manager.</param>
		/// <returns>Returns user identity claims.</returns>
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

			// NOTE: Add custom user claims here
			return userIdentity;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>
		/// Determines whether the specified <see cref="User" />, is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="User" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="User" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		protected bool Equals(User other)
		{
			return Equals(Profile, other.Profile);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return Profile?.GetHashCode() ?? 0;
		}
	}
}
