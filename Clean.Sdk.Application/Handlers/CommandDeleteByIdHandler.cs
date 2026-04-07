using AutoMapper;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using Clean.Sdk.Domain.Services;
using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Generic abstract class for delete an entity by Id
	/// </summary>
	public abstract class CommandDeleteByIdHandler<TRequest, TModel, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest>
		where TRequest : ICommandDeleteById, IRequest
		where TModel : class, IDomainModel
		where TServie : class, IDeleteService<TModel>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CommandDeleteByIdHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		/// <summary>
		/// Handle the request
		/// </summary>
		public async Task Handle(TRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var deleted = await Service.DeleteByIdAsync(request.Id, cancellationToken);
				if (!deleted)
				{
					throw new NotFoundException(Messages.NotFoundByIdException, typeof(TModel).Name, request.Id);
				}
			}
			catch (Exception)
			{
				throw;
			}

		}
	}
}
