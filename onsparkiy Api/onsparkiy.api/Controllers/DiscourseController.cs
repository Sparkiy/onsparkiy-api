using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using onsparkiy.api.DAL.Repositories;

namespace onsparkiy.api.Controllers
{
	/// <summary>
	/// Discourse SSO controller.
	/// </summary>
	public class DiscourseController : ApiController
	{
		private readonly ProfileRepository profileRepository;


		/// <summary>
		/// Initializes a new instance of the <see cref="DiscourseController"/> class.
		/// </summary>
		public DiscourseController()
		{
			this.profileRepository = new ProfileRepository();
		}


		/// <summary>
		/// Handles retrieving user for discourse sso.
		/// </summary>
		/// <param name="sso">The sso.</param>
		/// <param name="sig">The sig.</param>
		/// <returns>Returns the full redirect URL if user profile was retrieved successfully.</returns>
		/// <remarks>
		/// Source: https://gist.github.com/paully21/9232979
		/// </remarks>
		public async Task<IHttpActionResult> DiscourseLogin([FromUri] string sso, [FromUri] string sig)
		{
			// Validate view model
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				// Obtain discourse sso secret
				var ssoSecret = ConfigurationManager.AppSettings["TalkSsoSecret"];

				// Check checksum
				var checksum = GetHash(sso, ssoSecret);
				if (checksum != sig) return BadRequest("Invalid");

				// Retrieve sso
				var ssoBytes = Convert.FromBase64String(sso);
				var decodedSso = Encoding.UTF8.GetString(ssoBytes);

				// Retrieve nonce from query
				var nvc = HttpUtility.ParseQueryString(decodedSso);
				var nonce = nvc["nonce"];

				// Obtain username from token claim
				var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
				if (principal == null) return BadRequest("Coudln't retrieve token data.");
				var userName = principal.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
				if (userName == null) return BadRequest("Couldn't retrieve name identifier.");

				// Retrieve user profile by user name
				var profile = await this.profileRepository.GetProfileAsync(userName);
				if (profile == null) return BadRequest("Failed to retrieve profile for user name \"" + userName + "\"");

				// Retrieve required properties from profile
				var email = profile.User.Email;
				var username = profile.User.UserName;
				var externalId = profile.User.Id;

				// Build payload
				var returnPayload = $"nonce={nonce}&email={email}&external_id={externalId}&username={username}";

				// Construct returning signature (using payload and secret)
				var encodedPayload = Convert.ToBase64String(Encoding.UTF8.GetBytes(returnPayload));
				var returnSig = GetHash(encodedPayload, ssoSecret);

				// Build resirect URL
				var redirectUrl = $"{ConfigurationManager.AppSettings["TalkSsoRedirect"]}?sso={encodedPayload}&sig={returnSig}";

				return Ok(redirectUrl);
			}
			catch (Exception)
			{
				return BadRequest("Unknown error ocured. Check configuration.");
			}
		}

		/// <summary>
		/// Gets the hash of SSO.
		/// </summary>
		/// <param name="payload">The payload.</param>
		/// <param name="ssoSecret">The sso secret.</param>
		/// <returns>Returns the hash of given payload.</returns>
		private static string GetHash(string payload, string ssoSecret)
		{
			var keyBytes = new UTF8Encoding().GetBytes(ssoSecret);
			var bytes = new UTF8Encoding().GetBytes(payload);
			var hasher = new HMACSHA256(keyBytes);
			var hash = hasher.ComputeHash(bytes);

			return hash.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
		}
	}
}
