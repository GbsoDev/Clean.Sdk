using Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands;

namespace Clean.Sdk.Application.Tests.DataBuilders
{
	public class UpdateClientTestCommandBuilder
	{
		private Guid _id = Guid.NewGuid(); // Valor predeterminado para pruebas
		private string _name = "DefaultName";
		private string? _middleName = null;
		private string _surname = "DefaultSurname";
		private short _age = 30;

		public UpdateClientTestCommandBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public UpdateClientTestCommandBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public UpdateClientTestCommandBuilder WithMiddleName(string? middleName)
		{
			_middleName = middleName;
			return this;
		}

		public UpdateClientTestCommandBuilder WithSurname(string surname)
		{
			_surname = surname;
			return this;
		}

		public UpdateClientTestCommandBuilder WithAge(short age)
		{
			_age = age;
			return this;
		}

		public UpdateClientTestCommand Build()
		{
			return new UpdateClientTestCommand(_id, _name, _middleName, _surname, _age);
		}
	}
}
