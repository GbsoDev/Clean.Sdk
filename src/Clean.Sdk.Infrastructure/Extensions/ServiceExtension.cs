using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Helpers;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Viaje.PostVenta.Infrastructure.Extensions;

/// <summary>
/// Provider for registering domain services in the service collection.
/// </summary>
public static class ServiceExtension
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
		services.AddObjectService(assembly, ServiceType.Service);

		return services;
	}

	/// <summary>
	/// Adds repositories from the specified assembly to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="assembly">The assembly to scan for repositories marked with <see cref="RepositoryAttribute"/>.</param>
	/// <returns>The updated service collection.</returns>
	/// <exception cref="AppExeption">Thrown when a repository does not have a corresponding interface.</exception>
	public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
	{
		services.AddObjectService(assembly, ServiceType.Repository);
		return services;
	}

	private static void AddObjectService(this IServiceCollection services, Assembly assembly, ServiceType objectType)
	{
		var attributeType = objectType switch
		{
			ServiceType.Service => typeof(ServiceAttribute),
			ServiceType.Repository => typeof(RepositoryAttribute),
			_ => throw new AppExeption($"ServiceType {objectType} not found")
		};

		foreach (var type in SdkInfrastructureAssembly.GetAssemblyClassTypesByAttribute(assembly, attributeType))
		{
			var @interface = type.GetInterface(type.BuildInterfaceName());
			services.AddScoped(type);
			if (@interface != null)
			{
				services.AddScoped(@interface, type);
			}
			else
			{
				throw new AppExeption($"{objectType} {type.Name} has no interface");
			}
		}
	}

	private enum ServiceType : short
	{
		Service,
		Repository
	}
}
