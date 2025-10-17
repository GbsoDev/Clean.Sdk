namespace Clean.Sdk.Domain.Entity
{
	//public interface IDomainEntity<TId>
	public interface IDomainEntity
	//	where TId : struct
	{
		//TId Id { get; }
		object Id { get; }
	}
}
