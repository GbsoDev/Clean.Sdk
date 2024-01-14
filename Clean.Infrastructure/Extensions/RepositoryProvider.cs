using Clean.Domain.Ports;
using Clean.Domain.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{

	public static class DataRepositoryProvider
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
		{
			services.AddTransient(typeof(Lazy<>), typeof(LazyServiceProvider<>));
			foreach (var type in AssemblyExtractor.GeyTypesOfAssemblyByAttribute(assembly, typeof(RepositoryAttribute)))
			{
				var @interface = type.GetInterfaces().First(x => x.Name.Contains(type.Name));
				services.AddScoped(@interface, type);
			}
			return services;
		}
	}
}
