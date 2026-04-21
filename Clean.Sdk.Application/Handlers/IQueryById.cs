using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Defines the interface for a query that retrieves an entity by its identifier.
	/// </summary>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	public interface IQueryById<TResponse> : IRequest<TResponse>
	{
		/// <summary>
		/// Gets the identifier of the entity to be retrieved.
		/// </summary>
		object Id { get; }
	}
}
