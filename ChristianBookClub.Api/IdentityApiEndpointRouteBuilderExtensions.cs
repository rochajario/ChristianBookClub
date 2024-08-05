using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Api
{
	public static class IdentityApiEndpointRouteBuilderExtensions
	{
		// Validate the email address using DataAnnotations like the UserValidator does when RequireUniqueEmail = true.
		private static readonly EmailAddressAttribute _emailAddressAttribute = new();

		/// <summary>
		/// Add endpoints for registering, logging in, and logging out using ASP.NET Core Identity.
		/// </summary>
		/// <typeparam name="TUser">The type describing the user. This should match the generic parameter in <see cref="UserManager{TUser}"/>.</typeparam>
		/// <param name="endpoints">
		/// The <see cref="IEndpointRouteBuilder"/> to add the identity endpoints to.
		/// Call <see cref="EndpointRouteBuilderExtensions.MapGroup(IEndpointRouteBuilder, string)"/> to add a prefix to all the endpoints.
		/// </param>
		/// <returns>An <see cref="IEndpointConventionBuilder"/> to further customize the added endpoints.</returns>
		public static IEndpointConventionBuilder MapCustomIdentityApi<TUser>(this IEndpointRouteBuilder endpoints)
			where TUser : class, new()
		{
			ArgumentNullException.ThrowIfNull(endpoints);

			var timeProvider = endpoints.ServiceProvider.GetRequiredService<TimeProvider>();
			var bearerTokenOptions = endpoints.ServiceProvider.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();
			var emailSender = endpoints.ServiceProvider.GetRequiredService<IEmailSender<TUser>>();
			var linkGenerator = endpoints.ServiceProvider.GetRequiredService<LinkGenerator>();

			// We'll figure out a unique endpoint name based on the final route pattern during endpoint generation.
			string? confirmEmailEndpointName = null;

			var routeGroup = endpoints.MapGroup("");

			routeGroup.MapPost("/login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
				([FromBody] LoginRequest login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp) =>
			{
				var signInManager = sp.GetRequiredService<SignInManager<TUser>>();

				var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
				var isPersistent = (useCookies == true) && (useSessionCookies != true);
				signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

				var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

				if (result.RequiresTwoFactor)
				{
					if (!string.IsNullOrEmpty(login.TwoFactorCode))
					{
						result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
					}
					else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
					{
						result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
					}
				}

				if (!result.Succeeded)
				{
					return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
				}

				// The signInManager already produced the needed response in the form of a cookie or bearer token.
				return TypedResults.Empty;
			});

			routeGroup.MapPost("/refresh", async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult, SignInHttpResult, ChallengeHttpResult>>
				([FromBody] RefreshRequest refreshRequest, [FromServices] IServiceProvider sp) =>
			{
				var signInManager = sp.GetRequiredService<SignInManager<TUser>>();
				var refreshTokenProtector = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
				var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

				// Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
				if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
					timeProvider.GetUtcNow() >= expiresUtc ||
					await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not TUser user)

				{
					return TypedResults.Challenge();
				}

				var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
				return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
			});

			return new IdentityEndpointsConventionBuilder(routeGroup);
		}


		// Wrap RouteGroupBuilder with a non-public type to avoid a potential future behavioral breaking change.
		private sealed class IdentityEndpointsConventionBuilder(RouteGroupBuilder inner) : IEndpointConventionBuilder
		{
			private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

			public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);
			public void Finally(Action<EndpointBuilder> finallyConvention) => InnerAsConventionBuilder.Finally(finallyConvention);
		}

		[AttributeUsage(AttributeTargets.Parameter)]
		private sealed class FromBodyAttribute : Attribute, IFromBodyMetadata
		{
		}

		[AttributeUsage(AttributeTargets.Parameter)]
		private sealed class FromServicesAttribute : Attribute, IFromServiceMetadata
		{
		}

		[AttributeUsage(AttributeTargets.Parameter)]
		private sealed class FromQueryAttribute : Attribute, IFromQueryMetadata
		{
			public string? Name => null;
		}
	}
}
