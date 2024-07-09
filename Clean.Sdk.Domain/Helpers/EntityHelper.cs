using Clean.Domain.Entity;
using System;

namespace Clean.Domain.Helpers
{
	public class EntityHelper
	{
		public static bool IsIAuditableEntity<T>()
		{
			return typeof(IAuditableEntity).IsAssignableFrom(typeof(T));
		}

		public static bool IsIAuditableEntity<T>(T entityObject)
		{
			return IsIAuditableEntity<T>();
		}

		public static bool IsIAuditableEntity(Type? entityType)
		{
			return typeof(IAuditableEntity).IsAssignableFrom(entityType);
		}
	}
}
