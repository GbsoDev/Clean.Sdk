using Clean.Domain.Entity.Test.TestModel;
using Clean.Domain.Entity.Validations;

namespace Maios.CRM.Domain.Models.Validators
{
	public class UserValidator : EntityValidator<User>
	{
		public UserValidator()
		{
		}

		public override EntityValidationResult ValidateToCreate(User entity)
		{
			if (string.IsNullOrWhiteSpace(entity.Name))
			{
				ValidationResult.AddError(string.Format(EntityValidationMessages.RequiredValidationError, nameof(entity.Name)));
			}
			if (string.IsNullOrWhiteSpace(entity.Email))
			{
				ValidationResult.AddError(string.Format(EntityValidationMessages.RequiredValidationError, nameof(entity.Email)));
			}
			return ValidationResult;
		}

		public override EntityValidationResult ValidateToUpdate(User entity)
		{
			if (Guid.Empty == entity.Id)
			{
				ValidationResult.AddError(string.Format(EntityValidationMessages.RequiredValidationError, nameof(entity.Id)));
			}
			ValidateToCreate(entity);
			return ValidationResult;
		}

		public EntityValidationResult ValidateToAlternative(User entity)
		{
			if (string.IsNullOrWhiteSpace(entity.Name))
			{
				ValidationResult.AddError(string.Format(EntityValidationMessages.RequiredValidationError, nameof(entity.Name)));
			}
			return ValidationResult;
		}
	}
}
