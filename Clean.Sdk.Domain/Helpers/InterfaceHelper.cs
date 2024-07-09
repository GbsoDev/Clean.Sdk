using System;

namespace Clean.Sdk.Domain.Helpers
{
	public static class InterfaceHelper
	{
		public const string INTERFACE_PREFIX = "I";

		public static string BuildInterfaceName(string typeName)
		{
			return INTERFACE_PREFIX + typeName;
		}

		public static string BuildInterfaceName(this Type typeName)
		{
			return BuildInterfaceName(typeName.Name);
		}

		public static string BuildInterfaceName<T>() where T : class
		{
			return typeof(T).BuildInterfaceName();
		}

		public static string BuildInterfaceName<T>(T obj) where T : class
		{
			return BuildInterfaceName<T>();
		}
	}
}
