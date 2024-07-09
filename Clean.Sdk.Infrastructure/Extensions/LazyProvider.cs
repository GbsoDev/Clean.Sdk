using Microsoft.Extensions.DependencyInjection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	public static class LazyProvider
	{
		public static IServiceCollection AddLazySupport(this IServiceCollection services)
		{
			services.AddTransient(typeof(Lazy<>), typeof(LazyServiceProvider<>));
			return services;
		}
	}
}