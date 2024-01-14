namespace Maios.CRM.Application.Interfaces
{
	public abstract class CommandDeleteById<TId> : ICommandDeleteById
		where TId : struct
	{
		public TId Id { get; }
		object ICommandDeleteById.Id => Id;

		protected CommandDeleteById(TId id)
		{
			Id = id;
		}
	}
}