using System.Reflection;

namespace Clean.Sdk.Infrastructure.Utilities
{
	/// <summary>
	/// Helper class for working with assemblies.
	/// </summary>
	public static class SdkInfrastructureAssembly
	{
		/// <summary>
		/// Gets class types from the specified assembly that are decorated with the specified attribute.
		/// </summary>
		/// <param name="assembly">The assembly to search for types.</param>
		/// <param name="attributeType">The type of the attribute to look for.</param>
		/// <returns>An enumerable collection of class types that have the specified attribute.</returns>
		public static IEnumerable<Type> GetAssemblyClassTypesByAttribute(Assembly assembly, Type attributeType)
		{
			var types = assembly.GetTypes()
				.Where(type => type.IsClass && type.CustomAttributes.Any(attribute => attribute.AttributeType == attributeType));
			return types;
		}

		/// <summary>
		/// Gets interface types from the specified assembly that are decorated with the specified attribute.
		/// </summary>
		/// <param name="assembly">The assembly to search for types.</param>
		/// <param name="attributeType">The type of the attribute to look for.</param>
		/// <returns>An enumerable collection of class types that have the specified attribute.</returns>
		public static IEnumerable<Type> GetAssemblyInterfaceTypesByAttribute(Assembly assembly, Type attributeType)
		{
			var types = assembly.GetTypes()
				.Where(type => type.IsInterface && type.CustomAttributes.Any(attribute => attribute.AttributeType == attributeType));
			return types;
		}
	}
}
