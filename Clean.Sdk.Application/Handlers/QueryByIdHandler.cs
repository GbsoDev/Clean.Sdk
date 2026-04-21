using AutoMapper;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Generic abstract class for handling queries that retrieve an entity by its identifier.
	/// </summary>
	/// <typeparam name="TRequest">The type of the request query.</typeparam>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class QueryByIdHandler<TRequest, TResponse, TModel, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : class, IQueryById<TResponse>, IRequest<TResponse>
		where TModel : class, IDomainModel
		where TRepository : class, IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="QueryByIdHandler{TRequest, TResponse, TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="repository">The repository used to retrieve the entity.</param>
		protected QueryByIdHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		/// <summary>
		/// Handles the query by identifier request.
		/// </summary>
		/// <param name="request">The query request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation, containing the mapped response.</returns>
		/// <exception cref="NotFoundException">Thrown when the entity with the specified identifier is not found.</exception>
		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var id = request.Id;
			var entityResult = await Repository.GetByIdAsync(id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(string.Format(Messages.NotFoundByIdException, typeof(TModel).Name, id));
			return Mapper.Map<TModel, TResponse>(entityResult);
		}
	}
}
