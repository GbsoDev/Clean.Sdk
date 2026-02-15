using Clean.Sdk.Data.EfCore.Entities;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;

namespace Clean.Sdk.Data.EfCore.Tests.TestEntities.ClientsTest;

	public class ClientTestEntity : ClientTest, IDomainEntity
	{
		public ClientTestEntity(string name, string? middleName, string surname, short age)
			: base(name, middleName, surname, age)
		{
		}

		public ClientTestEntity(Guid id, string name, string? middleName, string surname, short age)
			: base(id, name, middleName, surname, age)
		{
		}

		object IDomainModel.Id => Id;
	}
