using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	public interface IQueryById<TResponse> : IRequest<TResponse>
	{
		object Id { get; }
	}
}
