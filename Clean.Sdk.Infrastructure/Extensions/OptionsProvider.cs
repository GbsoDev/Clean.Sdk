using Clean.Domain.Helpers;
using Clean.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Clean.Infrastructure.Extensions
{
	public static class OptionsProvider
	{
		/// <summary>
		/// Ruta de archivos de configuración: appsettings.json
		/// </summary>
		public static string? ConfigurationFilePath { get; set; }
		private const string DEVELOPMENT_SUFIX = "Development";
		private static string ConfigurationDevelopmentFilePath => $"{Path.GetFileNameWithoutExtension(ConfigurationFilePath)}.{DEVELOPMENT_SUFIX}{Path.GetExtension(ConfigurationFilePath)}";
		private static Action<BinderOptions> BinderOptions => options => options.BindNonPublicProperties = true;

		public static IServiceCollection ConfigureAppSettingOptions<T>(this IServiceCollection services, ref IConfiguration configuration, out T appSettings, bool singleton = true)
			where T : AppSettings, new()
		{
			appSettings = new T();
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
				.AddConfiguration(configuration);
			if (ConfigurationFilePath is not null)
			{
				configurationBuilder = configurationBuilder.AddJsonFile(ConfigurationFilePath, true, true);
#if (DEBUG)
				configurationBuilder = configurationBuilder.AddJsonFile(ConfigurationDevelopmentFilePath, true, true);
#endif
			}
			configurationBuilder = configurationBuilder.AddEnvironmentVariables();

			configuration = configurationBuilder.Build();
			configuration.Bind(appSettings, BinderOptions);

			services.AddSingleton(configuration);
			services.AddSingleton((IConfigurationRoot)configuration);
			if (singleton)
			{
				services.ConfigureSingletonOptions(configuration, AssemblyHelper.GetCleanDomainAssembly);
				services.Configure<T>(configuration, BinderOptions);
			}
			else
			{
				services.ConfigureScopedOptions(AssemblyHelper.GetCleanDomainAssembly);
				services.AddScoped<IOptions<T>>(serviceProvider => serviceProvider.BindOptions<T>());
			}
			return services;
		}

		public static IServiceCollection ConfigureScopedOptions(this IServiceCollection services, Assembly assembly)
		{
			return services.ConfigureOptions(null, assembly);
		}

		public static IServiceCollection ConfigureSingletonOptions(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
		{
			return services.ConfigureOptions(configuration, assembly);
		}

		public static void ConfigureSingleton<TOptions>(IServiceCollection services, IConfiguration configuration, string? secctionName = null)
			where TOptions : class, new()
		{
			var configurationSection = string.IsNullOrEmpty(secctionName) ? configuration : configuration.GetSection(secctionName);
			services.Configure<TOptions>(option => configurationSection.Bind(option, BinderOptions));
		}

		public static void ConfigureScoped<TOptions>(IServiceCollection services, string secctionName)
			where TOptions : class, new()
		{
			services.AddScoped<IOptions<TOptions>>(serviceProvider => serviceProvider.BindOptions<TOptions>(secctionName));
		}

		private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration? configuration, Assembly assembly)
		{
			var types = AssemblyHelper.GeyTypesByAttribute(assembly, typeof(OptionAttribute));
			foreach (var type in types)
			{
				var attribute = type.GetCustomAttribute<OptionAttribute>()!;
				var constructor = type.GetConstructor(new Type[] { });
				if (constructor != null)
				{
					if (configuration != null)
					{
						services.ConfigureSingleton(type, configuration, attribute.SecctionName);
					}
					else
					{
						services.ConfigureScoped(type, attribute.SecctionName);
					}
				}
			}
			return services;
		}

		private static void ConfigureSingleton(this IServiceCollection services, Type type, IConfiguration configuration, string? sectionName = null)
		{
			const string methodName = nameof(ConfigureSingleton);
			var classType = typeof(OptionsProvider);

			MethodInfo? configureMethod = classType
				.GetMethod(
					methodName,
					BindingFlags.Static | BindingFlags.NonPublic,
					null,
					new[] { typeof(IServiceCollection), typeof(IConfiguration), typeof(string) },
					null
				);

			if (configureMethod != null)
			{
				configureMethod.MakeGenericMethod(type)
					.Invoke(null, new object[] { services, configuration, sectionName! });
			}
			else
			{
				throw new Clean.Domain.Exceptions.AppExeption($"Error: Method {classType.FullName}.{methodName} not found");
			}
		}

		private static void ConfigureScoped(this IServiceCollection services, Type type, string? sectionName = null)
		{
			const string methodName = nameof(ConfigureScoped);
			var classType = typeof(OptionsProvider);

			MethodInfo? configureMethod = classType
				.GetMethod(
					methodName,
					BindingFlags.Static | BindingFlags.Public,
					null,
					new[] { typeof(IServiceCollection), typeof(string) },
					null
				);

			if (configureMethod != null)
			{
				configureMethod.MakeGenericMethod(type)
					.Invoke(null, new object[] { services, sectionName! });
			}
			else
			{
				throw new Clean.Domain.Exceptions.AppExeption($"Error: Method {classType.FullName}.{methodName} not found");
			}
		}

		private static CustomOption<TOptions> BindOptions<TOptions>(this IServiceProvider serviceProvider, string? sectionName = null) where TOptions : class, new()
		{
			var config = serviceProvider.GetRequiredService<IConfiguration>();
			config = string.IsNullOrEmpty(sectionName) ? config : config.GetSection(sectionName);

			return new(config.Get<TOptions>(BinderOptions)!);
		}
	}

	public class CustomOption<TOptions> : IOptions<TOptions> where TOptions : class
	{
		public TOptions Value { private set; get; }

		public CustomOption(TOptions value)
		{
			Value = value;
		}
	}
}