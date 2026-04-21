using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Defines the interface for a command that deletes an entity by its identifier.
	/// </summary>
	public interface ICommandDeleteById : IRequest
	{
		/// <summary>
		/// Gets the identifier of the entity to be deleted.
		/// </summary>
		public object Id { get; }
	}
}
