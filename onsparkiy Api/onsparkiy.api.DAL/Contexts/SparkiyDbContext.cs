using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using onsparkiy.api.DAL.Contexts.Contracts;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Contexts
{
	/// <summary>
	/// Sparkiy database context.
	/// </summary>
	public class SparkiyDbContext : IdentityDbContext<User>, ISparkiyDbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SparkiyDbContext"/> class.
		/// </summary>
		public SparkiyDbContext() : base("SparkiyDb")
		{
		}

		
		/// <summary>
		/// Gets or sets the profiles.
		/// </summary>
		/// <value>
		/// The profiles.
		/// </value>
		public DbSet<UserProfile> Profiles { get; set; }

		/// <summary>
		/// Creates the instance of <see cref="SparkiyDbContext"/>.
		/// </summary>
		/// <returns>
		/// Returns new instance of <see cref="SparkiyDbContext"/>.
		/// </returns>
		public static ISparkiyDbContext Create()
		{
			return new SparkiyDbContext();
		}
	}
}