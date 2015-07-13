using System.Threading.Tasks;
using onsparkiy.api.Models;

namespace onsparkiy.api.DAL.Repositories.Contracts
{
	/// <summary>
	/// Profile repository contract.
	/// </summary>
	public interface IProfileRepository : IRepository
	{
		/// <summary>
		/// Gets the profile.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>Returns instance of <see cref="UserProfile"/> for given username.</returns>
		Task<UserProfile> GetProfileAsync(string username);
	}
}