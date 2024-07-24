using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using System;

namespace Clean.Sdk.Domain.Tests.TestEntites.Clients
{
	public class ClientTestBuilder
	{
		private Guid _id = Guid.Empty;
		private string _name = "Default Name";
		private string? _middleName = null;
		private string _surname = "Default Surname";
		private short _age = 30;

		public ClientTestBuilder()
		{
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

		public ClientTest BuildForCreation()
		{
			return new ClientTest(_name, _middleName, _surname, _age);
		}

		public ClientTest Build()
		{
			return new ClientTest(_id, _name, _middleName, _surname, _age);
		}
	}
}
