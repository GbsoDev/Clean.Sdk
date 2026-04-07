using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;

namespace Clean.Sdk.Domain.Tests.TestServices.Clients
{
	internal class SaveClientTestService : SaveService<ClientTest, IRepository<ClientTest>>
	{
		public SaveClientTestService(ILoggerService logger, Lazy<IRepository<ClientTest>> repository) : base(logger, repository)
		{
		}
	}
}
