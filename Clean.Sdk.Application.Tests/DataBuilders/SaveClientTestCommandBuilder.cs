using Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands;

namespace Clean.Sdk.Application.Tests.DataBuilders
{
	public class SaveClientTestCommandBuilder
	{
		private string _name = "DefaultName";
		private string? _middleName = null;
		private string _surname = "DefaultSurname";
		private short _age = 30;

		public SaveClientTestCommandBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public SaveClientTestCommandBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public SaveClientTestCommandBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public SaveClientTestCommandBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public SaveClientTestCommand Build()
		{
			return new SaveClientTestCommand(_name, _middleName, _surname, _age);
		}
	}
}
