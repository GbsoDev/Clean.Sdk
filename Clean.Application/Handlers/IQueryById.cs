using MediatR;

namespace Clean.Application.Handlers
{
	public interface IQueryById : IRequest
	{
		object Id { get; }
	}
}
