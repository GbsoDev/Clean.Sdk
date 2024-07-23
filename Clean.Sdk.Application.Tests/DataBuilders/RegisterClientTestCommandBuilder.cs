using Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands;

namespace Clean.Sdk.Application.Tests.DataBuilders
{
	public class RegisterClientTestCommandBuilder
	{
		private string _name = "DefaultName";
		private string? _middleName = null;
		private string _surname = "DefaultSurname";
		private short _age = 30;

		public RegisterClientTestCommandBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public RegisterClientTestCommandBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public RegisterClientTestCommandBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public RegisterClientTestCommandBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public RegisterClientTestCommand Build()
		{
			return new RegisterClientTestCommand(_name, _middleName, _surname, _age);
		}
	}
}
