namespace Clean.Sdk.Domain.Tests.TestEntites.ClientsTest
{
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

		object IDomainEntity.Id => Id;
	}
}
