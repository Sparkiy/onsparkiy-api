using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using onsparkiy.api.DAL.Models;
using onsparkiy.api.DAL.Repositories;

namespace onsparkiy.api.Controllers
{
	/// <summary>
	/// Account controller.
	/// </summary>
	public class AccountController : ApiController
	{
		private readonly UserRepository userRepository;


		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		public AccountController()
		{
			this.userRepository = new UserRepository();
		}


		/// <summary>
		/// Registers the specified account.
		/// </summary>
		/// <param name="account">The account.</param>
		[HttpPost]
		[AllowAnonymous]
		public async Task<IHttpActionResult> Register(AccountViewModel account)
		{
			// Validate view model
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Register user
			var result = await this.userRepository.RegisterUserAsync(account);
			
			// Process registration result
			var errorResult = this.GetErrorResult(result);
			return errorResult ?? Ok();
		}

		/// <summary>
		/// Gets the error result.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <returns>Returns http result if error occured in identity result; null otherwise.</returns>
		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			// Internal server error if result is null
			if (result == null)
				return InternalServerError();

			// Return null if al is ok
			if (result.Succeeded)
				return null;
			
			// Add identity errors to the model state
			if (result.Errors != null)
				foreach (var error in result.Errors)
					this.ModelState.AddModelError("", error);

			// If no ModelState errors are available to send - return an empty BadRequest.
			if (this.ModelState.IsValid)
				return BadRequest();
			return BadRequest(this.ModelState);
		}
	}
}
