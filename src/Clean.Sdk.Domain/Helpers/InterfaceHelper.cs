namespace Clean.Sdk.Domain.Helpers
{
	/// <summary>
	/// Helper class for building interface names based on type names.
	/// </summary>
	public static class InterfaceHelper
	{
		/// <summary>
		/// The prefix used for interface names.
		/// </summary>
		public const string INTERFACE_PREFIX = "I";

		/// <summary>
		/// Builds an interface name by prepending the interface prefix to the specified type name.
		/// </summary>
		/// <param name="typeName">The name of the type.</param>
		/// <returns>The built interface name.</returns>
		public static string BuildInterfaceName(string typeName)
		{
			return INTERFACE_PREFIX + typeName;
		}

		/// <summary>
		/// Builds an interface name for the specified <see cref="Type"/>.
		/// </summary>
		/// <param name="typeName">The type for which to build the interface name.</param>
		/// <returns>The built interface name.</returns>
		public static string BuildInterfaceName(this Type typeName)
		{
			return BuildInterfaceName(typeName.Name);
		}

		/// <summary>
		/// Builds an interface name for the generic type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type for which to build the interface name.</typeparam>
		/// <returns>The built interface name.</returns>
		public static string BuildInterfaceName<T>() where T : class
		{
			return typeof(T).BuildInterfaceName();
		}

		/// <summary>
		/// Builds an interface name for the type of the specified object.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <param name="obj">The object for which to build the interface name.</param>
		/// <returns>The built interface name.</returns>
		public static string BuildInterfaceName<T>(T obj) where T : class
		{
			return BuildInterfaceName<T>();
		}
	}
}
