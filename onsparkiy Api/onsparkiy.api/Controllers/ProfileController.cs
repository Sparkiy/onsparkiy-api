using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using onsparkiy.api.DAL.Repositories;

namespace onsparkiy.api.Controllers
{
	/// <summary>
	/// Profile API controller.
	/// </summary>
	[Authorize]
	public class ProfileController : ApiController
	{
		private readonly ProfileRepository profileRepository;


		/// <summary>
		/// Initializes a new instance of the <see cref="ProfileController"/> class.
		/// </summary>
		public ProfileController()
		{
			this.profileRepository = new ProfileRepository();
		}


		/// <summary>
		/// Gets the profile for current user.
		/// </summary>
		/// <returns></returns>
		public async Task<IHttpActionResult> Get()
		{
			var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
			if (principal == null)
				return BadRequest("Coudln't retrieve token data.");

			var userName = principal.Claims.Single(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;

			return Ok(await this.profileRepository.GetProfileAsync(userName));
		}
	}
}
