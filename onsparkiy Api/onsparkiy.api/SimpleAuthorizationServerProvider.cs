﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using onsparkiy.api.DAL.Repositories;

namespace onsparkiy.api
{
	/// <summary>
	/// Simaple autorization server provider.
	/// </summary>
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		/// <summary>
		/// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are
		///             present on the request. If the web application accepts Basic authentication credentials,
		///             context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. If the web
		///             application accepts "client_id" and "client_secret" as form encoded POST parameters,
		///             context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body.
		///             If context.Validated is not called the request will not proceed further.
		/// 
		/// </summary>
		/// <param name="context">The context of the event carries information in and results out.</param>
		/// <returns>
		/// Task to enable asynchronous execution
		/// </returns>
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			// To add client auth, see: https://github.com/tjoudeh/AngularJSAuthentication/blob/master/AngularJSAuthentication.API/Providers/SimpleAuthorizationServerProvider.cs#L19

			// All clients are valid
			context.Validated();

			return Task.FromResult<object>(null);
		}

		/// <summary>
		/// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user has provided name and password
		///             credentials directly into the client application's user interface, and the client application is using those to acquire an "access_token" and
		///             optional "refresh_token". If the web application supports the
		///             resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To issue an
		///             access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated
		///             with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
		///             The default behavior is to reject this grant type.
		///             See also http://tools.ietf.org/html/rfc6749#section-4.3.2
		/// 
		/// </summary>
		/// <param name="context">The context of the event carries information in and results out.</param>
		/// <returns>
		/// Task to enable asynchronous execution
		/// </returns>
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			// Allow CORS
			if (!context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

			// Check if user exist
			using (var userRepository = new UserRepository())
			{
				var user = await userRepository.FindUserAsync(context.UserName, context.Password);
				if (user == null)
				{
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				}
			}

			// Create identity claims
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserName));
			identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

			// Create authentication properties
			var props = new AuthenticationProperties(new Dictionary<string, string>
			{
				{
					"sp:client_id", context.ClientId ?? string.Empty
				},
				{
					"userName", context.UserName
				}
			});

			// Create authentication ticket
			var ticket = new AuthenticationTicket(identity, props);

			// Validate ticket to the context
			context.Validated(identity);
		}

		/// <summary>
		/// Called when a request to the Token endpoint arrives with a "grant_type" of "refresh_token". This occurs if your application has issued a "refresh_token"
		///             along with the "access_token", and the client is attempting to use the "refresh_token" to acquire a new "access_token", and possibly a new "refresh_token".
		///             To issue a refresh token the an Options.RefreshTokenProvider must be assigned to create the value which is returned. The claims and properties
		///             associated with the refresh token are present in the context.Ticket. The application must call context.Validated to instruct the
		///             Authorization Server middleware to issue an access token based on those claims and properties. The call to context.Validated may
		///             be given a different AuthenticationTicket or ClaimsIdentity in order to control which information flows from the refresh token to
		///             the access token. The default behavior when using the OAuthAuthorizationServerProvider is to flow information from the refresh token to
		///             the access token unmodified.
		///             See also http://tools.ietf.org/html/rfc6749#section-6
		/// 
		/// </summary>
		/// <param name="context">The context of the event carries information in and results out.</param>
		/// <returns>
		/// Task to enable asynchronous execution
		/// </returns>
		public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
		{
			// Allow CORS
			if (!context.OwinContext.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
				context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

			// Change auth ticket for refresh token requests
			var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

			// Remove existing 'newClaim'
			var newClaim = newIdentity.Claims.FirstOrDefault(c => c.Type == "newClaim");
			if (newClaim != null)
			{
				newIdentity.RemoveClaim(newClaim);
			}

			// Add claim 'newClaim'
			newIdentity.AddClaim(new Claim("newClaim", "newValue"));

			// Clone ticket
			var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

			// Validate ticket to the context
			context.Validated(newTicket);

			// We dont use async/await mathods, return empty task
			return Task.FromResult<object>(null);
		}

		/// <summary>
		/// Called at the final stage of a successful Token endpoint request. An application may implement this call in order to do any final
		///             modification of the claims being used to issue access or refresh tokens. This call may also be used in order to add additional
		///             response parameters to the Token endpoint's json response body.
		/// 
		/// </summary>
		/// <param name="context">The context of the event carries information in and results out.</param>
		/// <returns>
		/// Task to enable asynchronous execution
		/// </returns>
		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (var property in context.Properties.Dictionary)
				context.AdditionalResponseParameters.Add(property.Key, property.Value);

			return Task.FromResult<object>(null);
		}
	}
}