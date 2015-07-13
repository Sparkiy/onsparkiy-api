using System;
using System.Data.Entity;
using System.Threading.Tasks;
using onsparkiy.api.DAL.Contexts;
using onsparkiy.api.DAL.Repositories.Contracts;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Repositories
{
	/// <summary>
	/// Profile repository.
	/// </summary>
	public class ProfileRepository : IProfileRepository
	{
		private bool isDisposed;

		/// <summary>
		/// Gets the context.
		/// </summary>
		/// <value>
		/// The context.
		/// </value>
		protected SparkiyDbContext Context { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="ProfileRepository"/> class.
		/// </summary>
		public ProfileRepository()
		{
			this.Context = new SparkiyDbContext();
		}


		/// <summary>
		/// Gets the profile.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>Returns instance of <see cref="UserProfile"/> for given username.</returns>
		public async Task<UserProfile> GetProfileAsync(string username)
		{
			var profile = await this.Context.Profiles.FirstOrDefaultAsync(p => p.User.UserName.Equals(username, StringComparison.InvariantCulture));

			return profile;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="ProfileRepository"/> class.
		/// </summary>
		~ProfileRepository()
		{
			this.Dispose(false);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		private void Dispose(bool disposing)
		{
			if (this.isDisposed)
				return;

			if (disposing)
			{
				// Dispose context
				this.Context?.Dispose();
			}

			this.isDisposed = true;
		}
	}
}