namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents a base query for retrieving an entity by its identifier.
	/// </summary>
	/// <typeparam name="TId">The type of the identifier.</typeparam>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	public abstract class QueryById<TId, TResponse> : IQueryById<TResponse>
		where TId : struct
	{
		/// <summary>
		/// Gets the identifier of the entity to be retrieved.
		/// </summary>
		public TId Id { get; }

		/// <summary>
		/// Gets the identifier of the entity as an object.
		/// </summary>
		object IQueryById<TResponse>.Id => Id;

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryById{TId, TResponse}"/> class.
		/// </summary>
		/// <param name="id">The identifier of the entity to be retrieved.</param>
		protected QueryById(TId id)
		{
			Id = id;
		}
	}
}