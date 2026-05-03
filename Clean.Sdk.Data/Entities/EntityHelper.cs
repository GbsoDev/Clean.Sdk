namespace Clean.Sdk.Data.Entities
{
	/// <summary>
	/// Helper class for working with entities.
	/// </summary>
	public class EntityHelper
	{
		/// <summary>
		/// Determines whether the specified type implements <see cref="IAuditableEntity"/>.
		/// </summary>
		/// <typeparam name="T">The type to check.</typeparam>
		/// <returns>true if the type implements <see cref="IAuditableEntity"/>; otherwise, false.</returns>
		public static bool IsIAuditableEntity<T>()
		{
			return typeof(IAuditableEntity).IsAssignableFrom(typeof(T));
		}

		/// <summary>
		/// Determines whether the specified entity object's type implements <see cref="IAuditableEntity"/>.
		/// </summary>
		/// <typeparam name="T">The type of the entity object.</typeparam>
		/// <param name="entityObject">The entity object to check.</param>
		/// <returns>true if the type implements <see cref="IAuditableEntity"/>; otherwise, false.</returns>
		public static bool IsIAuditableEntity<T>(T entityObject)
		{
			return IsIAuditableEntity<T>();
		}

		/// <summary>
		/// Determines whether the specified type implements <see cref="IAuditableEntity"/>.
		/// </summary>
		/// <param name="entityType">The type to check.</param>
		/// <returns>true if the type implements <see cref="IAuditableEntity"/>; otherwise, false.</returns>
		public static bool IsIAuditableEntity(Type? entityType)
		{
			return typeof(IAuditableEntity).IsAssignableFrom(entityType);
		}
	}
}
