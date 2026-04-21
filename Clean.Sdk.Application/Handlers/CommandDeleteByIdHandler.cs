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
	/// Generic abstract class for deleting an entity by its identifier.
	/// </summary>
	/// <typeparam name="TRequest">The type of the request command.</typeparam>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TServie">The type of the delete service.</typeparam>
	public abstract class CommandDeleteByIdHandler<TRequest, TModel, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest>
		where TRequest : ICommandDeleteById, IRequest
		where TModel : class, IDomainModel
		where TServie : class, IDeleteService<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandDeleteByIdHandler{TRequest, TModel, TServie}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="service">The delete service.</param>
		public CommandDeleteByIdHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		/// <summary>
		/// Handles the delete by identifier request.
		/// </summary>
		/// <param name="request">The delete command request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="NotFoundException">Thrown when the entity with the specified identifier is not found.</exception>
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
