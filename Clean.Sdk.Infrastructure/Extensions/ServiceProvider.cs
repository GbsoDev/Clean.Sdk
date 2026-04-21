using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Helpers;
using Clean.Sdk.Domain.Resources;
using Clean.Sdk.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for registering domain services in the service collection.
	/// </summary>
	public static class ServiceProvider
	{
		/// <summary>
		/// Adds domain services from the specified assembly to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan for services marked with <see cref="ServiceAttribute"/>.</param>
		/// <returns>The updated service collection.</returns>
		/// <exception cref="AppExeption">Thrown when a service does not have a corresponding interface.</exception>
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