using Clean.Sdk.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for MediatR configurations.
	/// </summary>
	public static class MediatRProvider
	{
		/// <summary>
		/// Adds MediatR services to the service collection, scanning the specified assembly for handlers.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan for handlers.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
		{
			services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
			return services;
		}
	}
}
