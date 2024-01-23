using Clean.Application.Mapper;
using Clean.Domain.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class AutoMapperProvider
	{
		public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
		{
			var types = AssemblyHelper.GeyTypesByAttribute(assembly, typeof(MapperProfileAttribute));
			services.AddAutoMapper(types.ToArray());
			return services;
		}
	}
}
