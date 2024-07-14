using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.TestEntites.ClientsTest
{

	public static class ClientTestValidator
	{
		public static ValidationSet ValidateToCreate(this ClientTest cliente)
		{
			ValidationSet validationSet = new ValidationSet(ValidationErrorMessages.InvalidEntityToCreate);

			validationSet
				.AddIsNotEmptyOrWhiteSpaceValidation(cliente.Name,
					ValidationErrorMessages.Required, nameof(cliente.Name))

				.AddLengthBetweenValidation(cliente.Name, ClientParameters.NameMinLength, ClientParameters.NameMaxLength,
					ValidationErrorMessages.Range, nameof(cliente.Name), ClientParameters.NameMinLength, ClientParameters.NameMaxLength)

				.AddValidation(cliente.MiddleName,
					middleName => middleName is null || middleName.LengthBetween(ClientParameters.NameMinLength, ClientParameters.NameMaxLength),
					ValidationErrorMessages.Range, nameof(cliente.MiddleName), ClientParameters.NameMinLength, ClientParameters.NameMaxLength)

				.AddIsNotEmptyOrWhiteSpaceValidation(cliente.Surname,
					ValidationErrorMessages.Required, nameof(cliente.Surname))

				.AddLengthBetweenValidation(cliente.Surname, ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength,
					ValidationErrorMessages.Range, nameof(cliente.Surname), ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength)

				.AddGreaterOrEqualToValidation(cliente.Age,
					ClientParameters.EageMin,
					ValidationErrorMessages.MinimumAge, ClientParameters.EageMin);
			return validationSet;
		}

		public static ValidationSet ValidateToUpdate(this ClientTest cliente)
		{
			ValidationSet ValidationResult = cliente.ValidateToCreate()
				.SetErrorMessage(ValidationErrorMessages.InvalidEntityToUpdate)
				.AddValidation(cliente.Id,
					id => id != Guid.Empty, string.Format(ValidationErrorMessages.Required, nameof(cliente.Id)));
			return ValidationResult;
		}
	}
}