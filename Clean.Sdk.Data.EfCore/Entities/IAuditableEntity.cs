namespace Clean.Sdk.Data.EfCore.Entities
{
	/// <summary>
	/// Represents an entity that is auditable, containing audit properties like save date and last update date.
	/// </summary>
	public interface IAuditableEntity : IDomainEntity
	{
	}
}
