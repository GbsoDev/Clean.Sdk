namespace Clean.Domain.Entity
{
	public abstract class DomainEntity<TId> : IDomainEntity
		where TId : struct
	{
		public TId Id { get; protected set; }

		object IDomainEntity.Id => Id;
	}

	public abstract class DomainEntity<TId, TValidator> : DomainEntity<TId>
		where TId : struct
		where TValidator : new()
	{
		protected static TValidator Validator => new TValidator();
	}
}
