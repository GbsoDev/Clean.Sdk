namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryById<TId, TResponse> : IQueryById<TResponse>
		where TId : struct
	{
		public TId Id { get; }
		object IQueryById<TResponse>.Id => Id;

		protected QueryById(TId id)
		{
			Id = id;
		}
	}
}