using AutoMapper;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using MediatR;
using System.Collections;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Generic abstract class for handling queries that retrieve a collection of entities.
	/// </summary>
	/// <typeparam name="TRequest">The type of the request query.</typeparam>
	/// <typeparam name="TResponse">The type of the response collection.</typeparam>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class QueryCollectionHandler<TRequest, TResponse, TModel, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : ICollection
		where TModel : class, IDomainModel
		where TRepository : class, IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="QueryCollectionHandler{TRequest, TResponse, TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="repository">The repository used to retrieve the entities.</param>
		public QueryCollectionHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		/// <summary>
		/// Handles the query collection request.
		/// </summary>
		/// <param name="request">The query request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation, containing the mapped response collection.</returns>
		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entityResult = await Repository.GetAllAsync(cancellationToken);
			return Mapper.Map<TModel[], TResponse>(entityResult);
		}
	}
}
