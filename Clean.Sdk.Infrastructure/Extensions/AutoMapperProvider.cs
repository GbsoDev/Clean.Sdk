using Clean.Sdk.Application.Mapper;
using Clean.Sdk.Domain.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for AutoMapper configurations.
	/// </summary>
	public static class AutoMapperProvider
	{
		/// <summary>
		/// Adds AutoMapper profiles from the specified assembly to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan for <see cref="MapperProfileAttribute"/>.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
		{
			var types = AssemblyHelper.GeyTypesByAttribute(assembly, typeof(MapperProfileAttribute));
			services.AddAutoMapper(types.ToArray());
			return services;
		}
	}
}
