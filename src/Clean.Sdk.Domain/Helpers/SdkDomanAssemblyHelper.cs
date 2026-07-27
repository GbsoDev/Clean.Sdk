using System.Reflection;

namespace Clean.Sdk.Domain.Helpers
{
	/// <summary>
	/// Helper class for working with assemblies.
	/// </summary>
	public static class SdkDomanAssemblyHelper
	{
		/// <summary>
		/// Gets the assembly that contains the current executing code.
		/// </summary>
		public static Assembly GetCleanDomainAssembly => Assembly.GetExecutingAssembly();
	}
}
