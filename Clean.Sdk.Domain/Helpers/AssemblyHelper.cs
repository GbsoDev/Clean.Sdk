using System.Reflection;

namespace Clean.Sdk.Domain.Helpers
{
	/// <summary>
	/// Helper class for working with assemblies.
	/// </summary>
	public static class AssemblyHelper
	{
		/// <summary>
		/// Gets the assembly that contains the current executing code.
		/// </summary>
		public static Assembly GetCleanDomainAssembly => Assembly.GetExecutingAssembly();

		/// <summary>
		/// Gets types from the specified assembly that are decorated with the specified attribute.
		/// </summary>
		/// <param name="assembly">The assembly to search for types.</param>
		/// <param name="TipoAtributo">The type of the attribute to look for.</param>
		/// <returns>An enumerable collection of types that have the specified attribute.</returns>
		public static IEnumerable<Type> GeyTypesByAttribute(Assembly assembly, Type TipoAtributo)
		{
			var types = assembly.GetTypes()
				.Where(type => type.CustomAttributes.Any(attribute => attribute.AttributeType == TipoAtributo));
			return types;
		}
	}
}
