using Clean.Domain.Helpers;
using Clean.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class OptionsProvider
	{
		private const string DEVELOPMENT_SUFIX = "Development";
		public static string ConfigurationFilePath { get; set; } = "appsettings.json";
		public static string ConfigurationDevelopmentFilePath { get => $"{Path.GetFileNameWithoutExtension(ConfigurationFilePath)}.{DEVELOPMENT_SUFIX}.{Path.GetExtension(ConfigurationFilePath)}"; }

		internal static IServiceCollection AddWebApiSettings<T>(this IServiceCollection services, ref IConfiguration configuration, out T appSettings)
			where T : AppSettings, new()
		{
			appSettings = new T();
			var builder = new ConfigurationBuilder();
			configuration = builder.AddConfiguration(configuration)
				.AddJsonFile(ConfigurationFilePath, true, true)
#if (DEBUG)
				.AddJsonFile(ConfigurationDevelopmentFilePath, true, true)
#endif
				.AddEnvironmentVariables()
				.Build();
			configuration.Bind(appSettings);
			services.AddSingleton(appSettings);
			return services;
		}

		public static void AddOptions(IServiceCollection services, IConfiguration configuration, Assembly assembly)
		{
			var types = AssemblyHelper.GeyTypesByAttribute(assembly, typeof(OptionAttribute));
			foreach (var type in types)
			{
				var attribute = type.GetCustomAttribute<OptionAttribute>()!;
				var constructor = type.GetConstructor(new Type[] { });
				if (constructor != null)
				{
					var instance = constructor.Invoke(null);
					AddOption(instance, services, configuration, attribute.SecctionName);
				}
			}
		}

		private static void AddOption<T>(T Instance, IServiceCollection services, IConfiguration configuration, string secctionName) where T : class
		{
			services.Configure<T>(instance => configuration.GetSection(secctionName).Bind(instance));
		}
	}
}