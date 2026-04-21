namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents a base command for deleting an entity by its identifier.
	/// </summary>
	/// <typeparam name="TId">The type of the identifier.</typeparam>
	public abstract class CommandDeleteById<TId> : ICommandDeleteById
		where TId : struct
	{
		/// <summary>
		/// Gets the identifier of the entity to be deleted.
		/// </summary>
		public TId Id { get; }

		/// <summary>
		/// Gets the identifier of the entity as an object.
		/// </summary>
		object ICommandDeleteById.Id => Id;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandDeleteById{TId}"/> class.
		/// </summary>
		/// <param name="id">The identifier of the entity to be deleted.</param>
		protected CommandDeleteById(TId id)
		{
			Id = id;
		}
	}
}