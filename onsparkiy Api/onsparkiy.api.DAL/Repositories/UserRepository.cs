using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using onsparkiy.api.DAL.Contexts;
using onsparkiy.api.DAL.Models;
using onsparkiy.api.DAL.Repositories.Contracts;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Repositories
{
	/// <summary>
	/// User repository.
	/// </summary>
	public class UserRepository : IUserRepository
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
		/// Gets the user store.
		/// </summary>
		/// <value>
		/// The user store.
		/// </value>
		protected UserStore<User> UserStore { get; }

		/// <summary>
		/// Gets the user manager.
		/// </summary>
		/// <value>
		/// The user manager.
		/// </value>
		protected UserManager<User> UserManager { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		public UserRepository()
		{
			this.Context = new SparkiyDbContext();
			this.UserStore = new UserStore<User>(this.Context);
			this.UserManager = new UserManager<User>(this.UserStore);
		}


		/// <summary>
		/// Finds the user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>Returns user instance that matches given authentication data.</returns>
		public async Task<User> FindUserAsync(string username, string password)
		{
			if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
				return null;

			var user = await this.UserManager.FindAsync(username, password);

			return user;
		}

		/// <summary>
		/// Registers the user.
		/// </summary>
		/// <param name="account">The account.</param>
		/// <returns>Returns result of identity storage create call.</returns>
		public async Task<IdentityResult> RegisterUserAsync(AccountViewModel account)
		{
			// Create user entity from view model
			var user = new User
			{
				UserName = account.Username,
				Email = account.Email,
				Profile = new UserProfile()
			};

			// Add user to identity storage
			var result = await this.UserManager.CreateAsync(user, account.Password);

			return result;
		}

		/// <summary>
		/// Checks if user with given username already exists.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>Returns <c>True</c> if user with given username already exists; <c>False</c> otherwise.</returns>
		public async Task<bool> ExistsAsync(string username)
		{
			if (string.IsNullOrEmpty(username))
				return false;

			return await this.UserManager.Users.AnyAsync(u => u.UserName.Equals(username));
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="UserRepository"/> class.
		/// </summary>
		~UserRepository()
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
				// Dispose user manager, user store and context
				this.UserManager?.Dispose();
				this.UserStore?.Dispose();
				this.Context?.Dispose();
			}

			this.isDisposed = true;
		}
	}
}
