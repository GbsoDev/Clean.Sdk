using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.TestEntites.Clients
{
	public class ClientTest : DomainEntity<Guid>, IAuditableEntity
	{
		public string Name { get; private set; }
		public string? MiddleName { get; private set; }
		public string Surname { get; private set; }
		public short Age { get; private set; }

		public ClientTest(string name, string? middleName, string surname, short age)
			: this(Guid.Empty, name, middleName, surname, age, ClientTestValidator.ValidateToCreate)
		{
		}

		public ClientTest(Guid id, string name, string? middleName, string surname, short age)
			: this(id, name, middleName, surname, age, ClientTestValidator.ValidateToUpdate)
		{
		}

		private ClientTest(Guid id, string name, string? middleName, string surname, short age, Func<ClientTest, ValidationSet> validateAction)
		{
			Id = id;
			Name = name.Trim();
			MiddleName = middleName?.Trim();
			Surname = surname.Trim();
			Age = age;
			validateAction.Invoke(this).ValidateAndThrow();
		}
	}
}
