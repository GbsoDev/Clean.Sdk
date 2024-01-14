using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Clean.Domain.Utilities


{
	public static class AssemblyExtractor
	{
		public static IEnumerable<Type> GeyTypesOfAssemblyByAttribute(Assembly assembly, Type TipoAtributo)
		{
			var types = assembly.GetTypes()
				.Where(x => x.CustomAttributes.Any(y => y.AttributeType == TipoAtributo));
			return types;
		}
	}
}


