using Clean.Domain.Utilities;
using Maios.CRM.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class AutoMapperProvider
	{
		public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
		{
			var types = AssemblyExtractor.GeyTypesOfAssemblyByAttribute(assembly, typeof(AutoMapperProfileAttribute));
			services.AddAutoMapper(types.ToArray());
			return services;
		}
	}
}
