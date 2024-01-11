using Clean.Domain.Entity.Validations;
using Maios.CRM.Domain.Entity;
using Maios.CRM.Domain.Models.Validators;

namespace Clean.Domain.Entity.Test.TestModel
{
    public class User : DomainEntity<Guid, UserValidator>
	{
		public string Name { get; protected set; }
		public string Email { get; protected set; }

		public User(string name, string email)
			: this(Guid.Empty, name, email, Validator.ValidateToCreateThrow)
		{
		}

		public User(Guid id, string name, string email)
			: this(id, name, email, Validator.ValidateToUpdateThrow)
		{
		}

		public User(string name)
			: this(Guid.Empty, name, string.Empty, Validator.ValidateToAlternativeThrow)
		{
		}

		protected User(Guid id, string name, string email, Action<User> validateAction)
		{
			Id = id;
			Name = name;
			Email = email;
			validateAction.Invoke(this);
		}
	}
}
