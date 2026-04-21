using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Helpers;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for registering data repositories in the service collection.
	/// </summary>
	public static class DataRepositoryProvider
	{
		/// <summary>
		/// Adds repositories from the specified assembly to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan for repositories marked with <see cref="RepositoryAttribute"/>.</param>
		/// <returns>The updated service collection.</returns>
		/// <exception cref="AppExeption">Thrown when a repository does not have a corresponding interface.</exception>
		public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
		{
			foreach (var type in AssemblyHelper.GeyTypesByAttribute(assembly, typeof(RepositoryAttribute)))
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
