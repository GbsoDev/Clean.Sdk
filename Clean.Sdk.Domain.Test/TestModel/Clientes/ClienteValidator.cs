using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Test.TestModel.Clientes
{

	public static class ClienteValidator
	{
		public static ValidationSet ValidateToCreate(this Cliente cliente)
		{
			ValidationSet validationSet = new ValidationSet(ValidationErrorMessages.InvalidEntityToCreate);

			validationSet
				.AddIsNotEmptyOrWhiteSpaceValidation(cliente.Nombre,
					ValidationErrorMessages.ElRequerdo, nameof(cliente.Nombre))

				.AddLengthBetweenValidation(cliente.Nombre, ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength,
					ValidationErrorMessages.ElRango, nameof(cliente.Nombre), ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength)

				.AddValidation(cliente.SegundoNombre,
					segundoNombre => segundoNombre is null || segundoNombre.LengthBetween(ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength),
					ValidationErrorMessages.ElRango, nameof(cliente.SegundoNombre), ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength)

				.AddIsNotEmptyOrWhiteSpaceValidation(cliente.Apellido,
					ValidationErrorMessages.ElRequerdo, nameof(cliente.Apellido))

				.AddLengthBetweenValidation(cliente.Apellido, ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength,
					ValidationErrorMessages.ElRango, nameof(cliente.Apellido), ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength)

				.AddValidation(cliente.SegundoApellido,
					segundoApellido => segundoApellido is null || segundoApellido.LengthBetween(ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength),
					ValidationErrorMessages.ElRango, nameof(cliente.SegundoApellido), ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength)

				.AddGreaterOrEqualToValidation(cliente.Edad,
					ClienteParametros.EdadMin,
					ValidationErrorMessages.EdadMenor, ClienteParametros.EdadMin);
			return validationSet;
		}

		public static ValidationSet ValidateToUpdate(this Cliente cliente)
		{
			ValidationSet ValidationResult = ValidateToCreate(cliente)
				.SetErrorMessage(ValidationErrorMessages.InvalidEntityToUpdate)
				.AddValidation(cliente.Id,
					id => id != Guid.Empty, string.Format(ValidationErrorMessages.ElRequerdo, nameof(cliente.Id)));
			return ValidationResult;
		}
	}
}