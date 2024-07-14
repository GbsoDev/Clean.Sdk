using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Tests.TestServices.ClientsTest
{
	internal class UpdateClientTestService : UpdateService<ClientTest, IRepository<ClientTest>>
	{
		public UpdateClientTestService(ILogger<Service> logger, Lazy<IRepository<ClientTest>> repository) : base(logger, repository)
		{
		}
	}
}
