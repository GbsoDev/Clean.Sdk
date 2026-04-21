using Clean.Sdk.Domain.Helpers;
using Clean.Sdk.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for application settings and options configurations.
	/// </summary>
	public static class OptionsProvider
	{
		/// <summary>
		/// Configuration file path: appsettings.json
		/// </summary>
		public static string? ConfigurationFilePath { get; set; }
		private const string DEVELOPMENT_SUFIX = "Development";
		private static string ConfigurationDevelopmentFilePath => $"{Path.GetFileNameWithoutExtension(ConfigurationFilePath)}.{DEVELOPMENT_SUFIX}{Path.GetExtension(ConfigurationFilePath)}";
		private static Action<BinderOptions> BinderOptions => options => options.BindNonPublicProperties = true;

		/// <summary>
		/// Configures application settings options from various sources including JSON files and environment variables.
		/// </summary>
		/// <typeparam name="T">The type of the application settings.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="configuration">The configuration instance.</param>
		/// <param name="appSettings">The resulting application settings object.</param>
		/// <param name="singleton">Indicates whether to register options as a singleton or scoped.</param>
		/// <returns>The updated service collection.</returns>
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

		/// <summary>
		/// Configures scoped options by scanning the specified assembly for <see cref="OptionAttribute"/>.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection ConfigureScopedOptions(this IServiceCollection services, Assembly assembly)
		{
			return services.ConfigureOptions(null, assembly);
		}

		/// <summary>
		/// Configures singleton options by scanning the specified assembly for <see cref="OptionAttribute"/>.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configuration">The configuration instance.</param>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection ConfigureSingletonOptions(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
		{
			return services.ConfigureOptions(configuration, assembly);
		}

		/// <summary>
		/// Configures a specific options type as a singleton.
		/// </summary>
		/// <typeparam name="TOptions">The type of options to configure.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="configuration">The configuration instance.</param>
		/// <param name="secctionName">The name of the configuration section.</param>
		public static void ConfigureSingleton<TOptions>(IServiceCollection services, IConfiguration configuration, string? secctionName = null)
			where TOptions : class, new()
		{
			var configurationSection = string.IsNullOrEmpty(secctionName) ? configuration : configuration.GetSection(secctionName);
			services.Configure<TOptions>(option => configurationSection.Bind(option, BinderOptions));
		}

		/// <summary>
		/// Configures a specific options type as scoped.
		/// </summary>
		/// <typeparam name="TOptions">The type of options to configure.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="secctionName">The name of the configuration section.</param>
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
				var constructor = type.GetConstructor(Array.Empty<Type>());
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
				throw new Clean.Sdk.Domain.Exceptions.AppExeption($"Error: Method {classType.FullName}.{methodName} not found");
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
				throw new Clean.Sdk.Domain.Exceptions.AppExeption($"Error: Method {classType.FullName}.{methodName} not found");
			}
		}

		private static CustomOption<TOptions> BindOptions<TOptions>(this IServiceProvider serviceProvider, string? sectionName = null) where TOptions : class, new()
		{
			var config = serviceProvider.GetRequiredService<IConfiguration>();
			config = string.IsNullOrEmpty(sectionName) ? config : config.GetSection(sectionName);

			return new(config.Get<TOptions>(BinderOptions)!);
		}
	}

	/// <summary>
	/// Custom implementation of <see cref="IOptions{TOptions}"/> for scoped options binding.
	/// </summary>
	/// <typeparam name="TOptions">The type of the options.</typeparam>
	public class CustomOption<TOptions> : IOptions<TOptions> where TOptions : class
	{
		/// <summary>
		/// Gets the configured options value.
		/// </summary>
		public TOptions Value { private set; get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomOption{TOptions}"/> class.
		/// </summary>
		/// <param name="value">The options value.</param>
		public CustomOption(TOptions value)
		{
			Value = value;
		}
	}
}