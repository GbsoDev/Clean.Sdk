using Clean.Application.Handlers;

namespace Maios.CRM.Application.Abstractions
{
	public abstract class QueryById<TId> : IQueryById
		where TId : struct
	{
		public TId Id { get; }
		object IQueryById.Id => Id;

		protected QueryById(TId id)
		{
			Id = id;
		}
	}
}