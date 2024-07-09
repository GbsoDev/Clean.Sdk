using Clean.Domain;
using Clean.Domain.Exceptions;
using Clean.Domain.Helpers;
using Clean.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class ServiceProvider
	{
		public static IServiceCollection AddDomainServices(this IServiceCollection services, Assembly assembly)
		{
			foreach (var type in AssemblyHelper.GeyTypesByAttribute(assembly, typeof(ServiceAttribute)))
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