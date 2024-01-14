﻿using Clean.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class MediatRProvider
	{
		public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
		{
			services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
			return services;
		}
	}
}