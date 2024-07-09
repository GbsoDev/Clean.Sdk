namespace Clean.Sdk.Application.Handlers
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