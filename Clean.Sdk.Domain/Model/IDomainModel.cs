namespace Clean.Sdk.Domain.Model
{
	/// <summary>
	/// Interface that defines the basic structure of a domain model.
	/// </summary>
	public interface IDomainModel
	{
		/// <summary>
		/// Gets the unique identifier of the domain model.
		/// </summary>
		object Id { get; }
	}
}
