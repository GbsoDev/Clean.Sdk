using Clean.Sdk.Application.Tests.TestHandlers.ClientsTest;

namespace Clean.Sdk.Application.Tests.DataBuilders
{
	public class ClientTestDtoBuilder
	{
		private Guid _id = Guid.Empty;
		private string _name = "DefaultName";
		private string? _middleName = null;
		private string _surname = "DefaultSurname";
		private short _age = 30;

		public ClientTestDtoBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ClientTestDtoBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ClientTestDtoBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public ClientTestDtoBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public ClientTestDtoBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public ClientTestDto BuildForCreation()
		{
			return new ClientTestDto(_name, _middleName, _surname, _age);
		}

		public ClientTestDto Build()
		{
			return new ClientTestDto(_id, _name, _middleName, _surname, _age);
		}
	}
}
