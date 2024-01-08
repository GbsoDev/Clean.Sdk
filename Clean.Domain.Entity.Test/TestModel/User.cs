using Clean.Domain.Entity.Validations;
using Maios.CRM.Domain.Entity;
using Maios.CRM.Domain.Models.Validators;

namespace Clean.Domain.Entity.Test.TestModel
{
    public class User : DomainEntity<Guid, UserValidator>
	{
		public string Name { get; protected set; }
		public string Email { get; protected set; }

		public User(string name)
			: this(Guid.Empty, name, string.Empty, ValidateToAlternative)
		{
		}

		public User(string name, string email)
			: this(Guid.Empty, name, email, ValidateToCreateThrow)
		{
		}

		public User(Guid id, string name, string email)
			: this(id, name, email, ValidateToUpdateThrow)
		{
		}

		protected User(Guid id, string name, string email, Action<IDomainEntity> validateAction)
			: base(validateAction)
		{
			Id = id;
			Name = name;
			Email = email;
			base.ValidateAction.Invoke(this);
		}

		protected static void ValidateToAlternative(IDomainEntity entity)
		{
			var validator = new UserValidator();
			var validationResult = validator.ValidateToAlternative((User)entity);
			if (!validationResult.IsValid)
			{
				throw new EntityValidationException(validationResult, string.Format(EntityValidationMessages.ValidateError, nameof(User)));
			}
		}
	}
}
