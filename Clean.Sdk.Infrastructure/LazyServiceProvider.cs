using Microsoft.Extensions.DependencyInjection;

namespace Clean.Sdk.Infrastructure
{
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
