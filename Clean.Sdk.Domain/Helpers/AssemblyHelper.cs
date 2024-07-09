using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Clean.Domain.Helpers
{
	public static class AssemblyHelper
	{
		public static Assembly GetCleanDomainAssembly => Assembly.GetExecutingAssembly();

		public static IEnumerable<Type> GeyTypesByAttribute(Assembly assembly, Type TipoAtributo)
		{
			var types = assembly.GetTypes()
				.Where(type => type.CustomAttributes.Any(attribute => attribute.AttributeType == TipoAtributo));
			return types;
		}
	}
}
