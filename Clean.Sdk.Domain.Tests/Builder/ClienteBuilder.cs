using Clean.Sdk.Domain.Tests.TestEntites.Clients;

namespace Clean.Sdk.Domain.Tests.Builders
{
	public sealed class ClientBuilder
	{
		private Guid _id;
		private string _name;
		private string? _middleName;
		private string _surname;
		private short _age;

		public ClientBuilder()
		{
			_id = Guid.NewGuid();
			_name = "Name";
			_middleName = null;
			_surname = "Surname";
			_age = 30;
		}

		public ClientBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ClientBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ClientBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public ClientBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public ClientBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public ClientTest BuildToCreate()
		{
			return new ClientTest(_name, _middleName, _surname, _age);
		}

		public ClientTest BuildToUpdate()
		{
			return new ClientTest(_id, _name, _middleName, _surname, _age);
		}
	}
}
