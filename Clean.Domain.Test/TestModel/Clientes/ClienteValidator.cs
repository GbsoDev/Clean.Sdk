using Clean.Domain.Validations;

namespace Clean.Domain.Test.TestModel.Clientes
{

	public static class ClienteValidator
	{
		public static ValidationSet ValidateToCreate(this Cliente cliente)
		{
			ValidationSet validationSet = new ValidationSet(ValidationErrorMessages.InvalidEntityToCreate);

			validationSet
				.AddValidation(cliente.Nombre,
					nombre => !string.IsNullOrWhiteSpace(nombre),
					string.Format(ValidationErrorMessages.ElRequerdo, nameof(cliente.Nombre)))
				.AddValidation(cliente.Nombre,
					nombre => nombre.Length.Between(ClienteParametros.NombreMinLength..ClienteParametros.NombreMaxLength),
					string.Format(ValidationErrorMessages.ElRango, nameof(cliente.Nombre), ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength))

				.AddValidation(cliente.SegundoNombre, 
					segundoNombre => segundoNombre is null || segundoNombre.Length.Between(ClienteParametros.NombreMinLength..ClienteParametros.NombreMaxLength),
					string.Format(ValidationErrorMessages.ElRango, nameof(cliente.SegundoNombre), ClienteParametros.NombreMinLength, ClienteParametros.NombreMaxLength))

				.AddValidation(cliente.Apellido,
					apellido => !string.IsNullOrWhiteSpace(apellido),
					string.Format(ValidationErrorMessages.ElRequerdo, nameof(cliente.Apellido)))

				.AddValidation(cliente.Apellido,
					apellido => apellido.Length.Between(ClienteParametros.ApellidoMinLength..ClienteParametros.ApellidoMaxLength),
					string.Format(ValidationErrorMessages.ElRango, nameof(cliente.Apellido), ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength))

				.AddValidation(cliente.SegundoApellido,
					segundoApellido => segundoApellido is null || segundoApellido.Length.Between(ClienteParametros.ApellidoMinLength..ClienteParametros.ApellidoMaxLength),
					string.Format(ValidationErrorMessages.ElRango, nameof(cliente.SegundoApellido), ClienteParametros.ApellidoMinLength, ClienteParametros.ApellidoMaxLength))

				.AddValidation(cliente.Edad,
					edad =>  edad >= 18, string.Format(ValidationErrorMessages.EdadMenor, 18));
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