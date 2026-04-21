using Clean.Sdk.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for Web API related configurations.
	/// </summary>
	public static class WebApiProvider
	{
		/// <summary>
		/// Adds JWT authentication to the service collection using the provided application settings.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="appSettings">The application settings containing authentication options.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddWebApiAutenticacionToken(this IServiceCollection services, AppSettings appSettings)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
			{
				o.Audience = appSettings.AuthOptions.Audience;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = appSettings.AuthOptions.Issuer,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.AuthOptions.SigningKey))
				};
			});
			return services;
		}

		/// <summary>
		/// Adds CORS policies to the service collection based on the application settings.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="appSettings">The application settings containing CORS policies.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddWebApiCorsPolicies(this IServiceCollection services, AppSettings appSettings)
		{
			return services.AddCors(options =>
			{
				foreach (var corPolicy in appSettings.AllowCors)
				{
					options.AddPolicy(corPolicy.Origin, builder =>
					{
						var policy = builder.WithOrigins(corPolicy.Origin)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();

						if (corPolicy.Methods?.Any() ?? false)
						{
							policy.WithMethods(corPolicy.Methods);
						}
					});
				}
			});
		}

		/// <summary>
		/// Configures the application to use CORS policies after the build process.
		/// </summary>
		/// <param name="app">The application builder.</param>
		/// <param name="appSettings">The application settings containing CORS policies.</param>
		/// <returns>The updated application builder.</returns>
		public static IApplicationBuilder AddPosBuildWebApiCorsPolicies(this IApplicationBuilder app, AppSettings appSettings)
		{
			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
			});

			foreach (var corPolicy in appSettings.AllowCors)
			{
				app.UseCors(corPolicy.Name);
			}
			return app;
		}
	}
}
