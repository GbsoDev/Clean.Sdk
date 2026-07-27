using Microsoft.Extensions.DependencyInjection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for adding support for lazy loading of services.
	/// </summary>
	public static class LazyLoadingExtension
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

	/// <summary>
	/// Represents a lazy-initialized service that is resolved from the <see cref="IServiceProvider"/>.
	/// </summary>
	/// <typeparam name="T">The type of the service.</typeparam>
	public class LazyServiceProvider<T> : Lazy<T>
		where T : notnull
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LazyServiceProvider{T}"/> class.
		/// </summary>
		/// <param name="serviceProvider">The service provider to resolve the service from.</param>
		public LazyServiceProvider(IServiceProvider serviceProvider) : base(() => serviceProvider.GetRequiredService<T>())
		{
		}
	}
}