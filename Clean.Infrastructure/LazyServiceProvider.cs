using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clean.Infrastructure
{
	public class LazyServiceProvider<T> : Lazy<T>
		where T : notnull
	{
		public LazyServiceProvider(IServiceProvider serviceProvider) : base(() => serviceProvider.GetRequiredService<T>())
		{
		}
	}
}
