using Microsoft.AspNet.Identity.EntityFramework;

namespace onsparkiy.api.Models.Contracts
{
	/// <summary>
	/// User contract.
	/// </summary>
	public interface IUser : IIdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
	{
		/// <summary>
		/// Gets or sets the profile.
		/// </summary>
		/// <value>
		/// The profile.
		/// </value>
		UserProfile Profile { get; set; }
	}
}