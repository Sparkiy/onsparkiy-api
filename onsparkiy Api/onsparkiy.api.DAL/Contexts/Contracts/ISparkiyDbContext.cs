using System.Data.Entity;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Contexts.Contracts
{
	/// Sparkiy database context contract.
	public interface ISparkiyDbContext
	{
		/// <summary>
		/// Gets or sets the profiles.
		/// </summary>
		/// <value>
		/// The profiles.
		/// </value>
		DbSet<UserProfile> Profiles { get; set; }
	}
}