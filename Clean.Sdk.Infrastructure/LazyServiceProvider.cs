using Microsoft.Extensions.DependencyInjection;

namespace Clean.Sdk.Infrastructure
{
	public class LazyServiceProvider<T> : Lazy<T>
		where T : notnull
	{
		public LazyServiceProvider(IServiceProvider serviceProvider) : base(() => serviceProvider.GetRequiredService<T>())
		{
		}
	}
}
