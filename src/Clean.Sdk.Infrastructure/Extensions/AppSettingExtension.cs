using Clean.Sdk.Domain.Attributes;
using Clean.Sdk.Infrastructure.Exceptions;
using Clean.Sdk.Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for application settings and options configurations.
	/// </summary>
	public static class AppSettingExtension
	{

		/// <summary>
		/// Configuration file path: appsettings.json
		/// </summary>
		private static Action<BinderOptions> BinderOptions => options => options.BindNonPublicProperties = true;



		/// <summary>
		/// Configures application settings options from various sources including JSON files and environment variables.
		/// </summary>
		/// <typeparam name="T">The providerType of the application settings.</typeparam>
		/// <param name="builder">The application builder.</param>
		/// <param name="assembly">The assembly to scan for options.</param>
		/// <param name="appSettings">The resulting application settings object.</param>
		/// <param name="singleton">Indicates whether to register options as a singleton or scoped.</param>
		/// <returns>The updated service collection.</returns>
		public static IHostApplicationBuilder AddAppSettingProviders<T>(this IHostApplicationBuilder builder, Assembly assembly, out T appSettings, bool singleton = true)
			where T : class, new()
		{
			IConfigurationManager configurationManager = builder.Configuration;
			IConfigurationBuilder configurationBuilder = builder.Configuration;
			IServiceCollection services = builder.Services;

			appSettings = new();

			configurationManager.Build().Bind(appSettings, BinderOptions);

			if (singleton)
			{
				services.AddSingletonSettings(configurationManager, assembly);
				services.Configure<T>(configurationManager, BinderOptions);
			}
			else
			{
				services.AddScopedSettings(assembly);
				services.AddScoped<IOptions<T>>(serviceProvider => serviceProvider.BindOptions<T>());
			}
			return builder;
		}

		/// <summary>
		/// Configures scoped options by scanning the specified assembly for <see cref="AppSettingProviderAttribute"/>.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddScopedSettings(this IServiceCollection services, Assembly assembly)
		{
			return services.ConfigureOptions(null, assembly);
		}

		/// <summary>
		/// Configures singleton options by scanning the specified assembly for <see cref="AppSettingProviderAttribute"/>.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configuration">The configuration instance.</param>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The updated service collection.</returns>
		public static IServiceCollection AddSingletonSettings(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
		{
			return services.ConfigureOptions(configuration, assembly);
		}

		/// <summary>
		/// Configures a specific options providerType as a singleton.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configuration">The configuration instance.</param>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The updated service collection.</returns>

		private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration? configuration, Assembly assembly)
		{
			var providertypes = SdkInfrastructureAssembly.GetAssemblyClassTypesByAttribute(assembly, typeof(AppSettingProviderAttribute));
			foreach (var providerType in providertypes)
			{
				var attribute = providerType.GetCustomAttribute<AppSettingProviderAttribute>()!;
				var constructor = providerType.GetConstructor(Array.Empty<Type>());
				if (constructor != null)
				{
					if (configuration != null)
					{
						services.ConfigureSingleton(providerType, configuration, attribute.SectionName);
					}
					else
					{
						services.ConfigureScoped(providerType, attribute.SectionName);
					}
				}
			}
			return services;
		}

		private static void ConfigureSingleton(this IServiceCollection services, Type type, IConfiguration configuration, string? sectionName = null)
		{
			const string methodName = nameof(AddSingleton);
			var appSettingExtensionType = typeof(AppSettingExtension);

			MethodInfo? configureMethod = appSettingExtensionType
				.GetMethod(
					methodName,
					BindingFlags.Static | BindingFlags.NonPublic,
					null,
					[typeof(IServiceCollection), typeof(IConfiguration), typeof(string)],
					null
				);

			if (configureMethod != null)
			{
				_ = configureMethod.MakeGenericMethod(type)
					.Invoke(null, [services, configuration, sectionName!]);
			}
			else
			{
				throw new ConfigurationException($"Error: Method {appSettingExtensionType.FullName}.{methodName} not found");
			}
		}

		private static void ConfigureScoped(this IServiceCollection services, Type type, string? sectionName = null)
		{
			const string methodName = nameof(AddScoped);
			var appSettingExtensionType = typeof(AppSettingExtension);

			MethodInfo? configureMethod = appSettingExtensionType
				.GetMethod(
					methodName,
					BindingFlags.Static | BindingFlags.NonPublic,
					null,
					[typeof(IServiceCollection), typeof(string)],
					null
				);

			if (configureMethod != null)
			{
				_ = configureMethod.MakeGenericMethod(type)
					.Invoke(null, [services, sectionName!]);
			}
			else
			{
				throw new ConfigurationException($"Error: Method {appSettingExtensionType.FullName}.{methodName} not found");
			}
		}

		private static void AddSingleton<TOptions>(IServiceCollection services, IConfiguration configuration, string? secctionName = null)
			where TOptions : class, new()
		{
			var configurationSection = string.IsNullOrEmpty(secctionName) ? configuration : configuration.GetSection(secctionName);
			services.Configure<TOptions>(option => configurationSection.Bind(option, BinderOptions));
		}

		/// <summary>
		/// Configures a specific options providerType as scoped.
		/// </summary>
		/// <typeparam name="TOptions">The providerType of options to configure.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="secctionName">The name of the configuration section.</param>
		private static void AddScoped<TOptions>(IServiceCollection services, string secctionName)
			where TOptions : class, new()
		{
			services.AddScoped<IOptions<TOptions>>(serviceProvider => serviceProvider.BindOptions<TOptions>(secctionName));
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
	/// <typeparam name="TOptions">The providerType of the options.</typeparam>
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