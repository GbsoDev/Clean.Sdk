using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	public interface ICommandDeleteById : IRequest
	{
		public object Id { get; }
	}
}
