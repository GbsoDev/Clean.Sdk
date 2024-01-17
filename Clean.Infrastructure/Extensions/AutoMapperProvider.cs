using Clean.Domain.Helpers;
using Maios.CRM.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class AutoMapperProvider
	{
		public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Assembly assembly)
		{
			var types = AssemblyHelper.GeyTypesByAttribute(assembly, typeof(AutoMapperProfileAttribute));
			services.AddAutoMapper(types.ToArray());
			return services;
		}
	}
}
