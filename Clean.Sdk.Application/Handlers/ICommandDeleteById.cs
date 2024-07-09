using MediatR;

namespace Clean.Application.Handlers
{
	public interface ICommandDeleteById : IRequest
	{
		public object Id { get; }
	}
}
