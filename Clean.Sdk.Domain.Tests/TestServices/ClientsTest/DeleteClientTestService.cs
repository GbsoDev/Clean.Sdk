using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Tests.TestServices.Clients
{
	internal class DeleteClientTestService : DeleteService<ClientTest, IRepository<ClientTest>>
	{
		public DeleteClientTestService(ILogger<Service> logger, Lazy<IRepository<ClientTest>> repository) : base(logger, repository)
		{
		}
	}
}
