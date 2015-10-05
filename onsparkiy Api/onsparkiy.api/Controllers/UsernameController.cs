using System;
using System.Threading.Tasks;
using System.Web.Http;
using onsparkiy.api.DAL.Repositories;

namespace onsparkiy.api.Controllers
{
	/// <summary>
	/// Username controller.
	/// </summary>
	public class UserNameController : ApiController
	{
		private readonly UserRepository userRepository;


		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		public UserNameController()
		{
			this.userRepository = new UserRepository();
		}


		/// <summary>
		/// Checks if given username already exists.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>Returns <c>True</c> if given username already exists; <c>False</c> otherwise.</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IHttpActionResult> IsTaken([FromUri] string username)
		{
			// Validate view model
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Check if username is valid
			if (string.IsNullOrEmpty(username))
				return BadRequest("username is empty.");

			try
			{
				return Ok(await this.userRepository.ExistsAsync(username));
			}
			catch (Exception ex)
			{
				return BadRequest("Failed to check if user name \"" + username + "\" already exists. Consider it taken. Error message: " + ex.Message);
			}
		}
	}
}
