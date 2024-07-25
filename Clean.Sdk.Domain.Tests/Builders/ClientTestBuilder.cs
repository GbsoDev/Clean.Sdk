using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;

namespace Clean.Sdk.Domain.Tests.Builders
{
	public sealed class ClientTestBuilder
	{
		private Guid _id;
		private string _name;
		private string? _middleName;
		private string _surname;
		private short _age;

		public ClientTestBuilder()
		{
			_id = Guid.NewGuid();
			_name = "Gerson";
			_middleName = null;
			_surname = "Sanchez";
			_age = 30;
		}

		public ClientTestBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ClientTestBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ClientTestBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public ClientTestBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public ClientTestBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public ClientTest Build()
		{
			return new ClientTest(_id, _name, _middleName, _surname, _age);
		}

		public ClientTest BuildToCreate()
		{
			return new ClientTest(_name, _middleName, _surname, _age);
		}
	}
}
