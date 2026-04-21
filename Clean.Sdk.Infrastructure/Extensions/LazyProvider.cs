using Microsoft.Extensions.DependencyInjection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for adding support for lazy loading of services.
	/// </summary>
	public static class LazyProvider
	{
		/// <summary>
		/// Adds support for <see cref="Lazy{T}"/> service resolution to the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddLazySupport(this IServiceCollection services)
		{
			services.AddTransient(typeof(Lazy<>), typeof(LazyServiceProvider<>));
			return services;
		}
	}
}