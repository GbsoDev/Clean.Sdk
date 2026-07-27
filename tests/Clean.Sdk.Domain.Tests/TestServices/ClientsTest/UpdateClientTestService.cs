using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;

namespace Clean.Sdk.Domain.Tests.TestServices.ClientsTest
{
	internal class UpdateClientTestService : UpdateService<ClientTest, IRepository<ClientTest>>
	{
		public UpdateClientTestService(ILoggerService logger, Lazy<IRepository<ClientTest>> repository) : base(logger, repository)
		{
		}
	}
}
