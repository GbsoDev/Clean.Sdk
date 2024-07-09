namespace Clean.Domain.Entity
{
	public abstract class DomainEntity<TId> : IDomainEntity
		where TId : struct
	{
		public TId Id { get; protected set; }

		object IDomainEntity.Id => Id;
	}
}
