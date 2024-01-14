using Clean.Domain;
using Clean.Domain.Exceptions;
using Clean.Domain.Helpers;
using Clean.Domain.Services;
using Clean.Domain.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class ServiceProvider
	{
		public static IServiceCollection AddDomainServices(this IServiceCollection services, Assembly assembly)
		{
			services.AddTransient(typeof(Lazy<>), typeof(LazyServiceProvider<>));
			foreach (var type in AssemblyExtractor.GeyTypesOfAssemblyByAttribute(assembly, typeof(ServiceAttribute)))
			{
				var @interface = type.GetInterface(type.BuildInterfaceName());
				services.AddScoped(type);
				if (@interface != null)
				{
					services.AddScoped(@interface, type);
				}
				else
				{
					throw new AppExeption(string.Format(Messages.ServiceHasNoInterface, type.Name));
				}
			}
			return services;
		}
	}
}