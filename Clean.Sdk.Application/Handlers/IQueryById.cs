using MediatR;

namespace Clean.Application.Handlers
{
	public interface IQueryById<TResponse> : IRequest<TResponse>
	{
		object Id { get; }
	}
}
