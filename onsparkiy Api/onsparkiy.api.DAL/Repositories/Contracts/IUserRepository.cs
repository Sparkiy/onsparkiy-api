using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Repositories.Contracts
{
	/// <summary>
	/// User repository contract.
	/// </summary>
	public interface IUserRepository : IRepository
	{
		/// <summary>
		/// Finds the user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>Returns user instance that matches given authentication data.</returns>
		Task<User> FindUserAsync(string username, string password);

		/// <summary>
		/// Registers the user.
		/// </summary>
		/// <param name="account">The account.</param>
		/// <returns>Returns result of identity storage create call.</returns>
		Task<IdentityResult> RegisterUserAsync(AccountViewModel account);

		/// <summary>
		/// Checks if user with given username already exists.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>Returns <c>True</c> if user with given username already exists; <c>False</c> otherwise.</returns>
		Task<bool> ExistsAsync(string username);
	}
}