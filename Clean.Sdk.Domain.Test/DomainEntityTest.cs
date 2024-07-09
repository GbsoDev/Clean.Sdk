using Clean.Sdk.Domain.Test.TestModel;
using Clean.Sdk.Domain.Test.TestModel.Clientes;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Entity.Test
{
	public class DomainEntityTest
	{
		private const string validNombre = "gerson";
		private const string validSegundoNombre = "brain";
		private const string validApellido = "sanchez";
		private const string validSegundoApellido = "ospina";
		private const short validEdad = 50;

		private const string textWith60Charters = "Lorem ipsum dolor sit amet, consectetur adipiscing elit est.";

		private static readonly string  expectedCreateValidationExceptionMessage = string.Format(ValidationErrorMessages.InvalidEntityToCreate, nameof(Cliente));
		private static readonly string  expectedUpdateValidationExceptionMessage = string.Format(ValidationErrorMessages.InvalidEntityToUpdate, nameof(Cliente));
		
		private static readonly string  expectedIdValidationErrorMessage = "";
		private static readonly string  expectedNombreValidationErrorMessage = "";
		private static readonly string  expectedNombreRangoValidationErrorMessage = "";
		private static readonly string	expectedSegundoNombreRangoValidationErrorMessage = "";
		private static readonly string  expectedApellidoValidationErrorMessage = "";
		private static readonly string  expectedApellidoRangoValidationErrorMessage = "";
		private static readonly string	expectedSegundoApellidoRangoValidationErrorMessage = "";
		private static readonly string  expectedEdadlValidationErrorMessage = "";


		[Fact]
		public void ValidateCreateEntity_Ok()
		{
			// Arrange
			var expectedNombre = validNombre;
			string? expectedSegundoNombre = null;
			var expectedApellido = validApellido;
			string? expectedSegundoApellido = null;
			var expectedEdad = validEdad;

			var clienteBuilder = new ClienteBuilder()
				.WithNombre(expectedNombre)
				.WithSegundoNombre(expectedSegundoNombre)
				.WithApellido(expectedApellido)
				.WithSegundoApellido(expectedSegundoApellido)
				.WithEdad(expectedEdad);

			// Act
			var newCLiente = clienteBuilder.BuildToCreate();

			// Assert
			Assert.Equal(expectedNombre, newCLiente.Nombre);
			Assert.Equal(expectedSegundoNombre, newCLiente.SegundoNombre);
			Assert.Equal(expectedApellido, newCLiente.Apellido);
			Assert.Equal(expectedSegundoApellido, newCLiente.SegundoApellido);
			Assert.Equal(expectedEdad, newCLiente.Edad);
		}

		[Fact]
		public void ValidateCreateEntity_Error()
		{
			// Arrange
			const int expectedNumValidationErrors = 7;

			var clienteBuilder = new ClienteBuilder()
				.WithNombre(string.Empty)
				.WithSegundoNombre(textWith60Charters)
				.WithApellido(string.Empty)
				.WithSegundoApellido(textWith60Charters)
				.WithEdad(5);

			// Act
			var exception = Assert.Throws<ValidationException>(() =>
			{
				clienteBuilder.BuildToCreate();
			});

			// Assert
			Assert.Equal(expectedCreateValidationExceptionMessage, exception.Message);
			Assert.True(exception.Errors.Length == expectedNumValidationErrors);
			Assert.Collection(exception.Errors,
				error => Assert.Equal(expectedNombreValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedNombreRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedSegundoNombreRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedApellidoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedApellidoRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedSegundoApellidoRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedEdadlValidationErrorMessage, error.Message)
			);
		}

		[Fact]
		public void ValidateUpdateEntity_Ok()
		{
			// Arrange
			var expectedId = Guid.NewGuid();
			var expectedNombre = validNombre;
			string? expectedSegundoNombre = validSegundoNombre;
			var expectedApellido = validApellido;
			var expectedSegundoApellido = validSegundoApellido;
			var expectedEdad = validEdad;

			var clienteBuilder = new ClienteBuilder()
				.WithId(expectedId)
				.WithNombre(expectedNombre)
				.WithSegundoNombre(expectedSegundoNombre)
				.WithApellido(expectedApellido)
				.WithSegundoApellido(expectedSegundoApellido)
				.WithEdad(expectedEdad);

			// Act
			var cliente = clienteBuilder.BuildToUpdate();

			// Assert
			Assert.Equal(expectedId, cliente.Id);
			Assert.Equal(expectedNombre, cliente.Nombre);
			Assert.Equal(expectedSegundoNombre, cliente.SegundoNombre);
			Assert.Equal(expectedApellido, cliente.Apellido);
			Assert.Equal(expectedSegundoApellido, cliente.SegundoApellido);
			Assert.Equal(expectedEdad, cliente.Edad);

		}

		[Fact]
		public void ValidateUpdateEntity_Error()
		{
			// Arrange
			const int expectedNumValidationErrors = 8;

			var clienteBuilder = new ClienteBuilder()
				.WithId(Guid.Empty)
				.WithNombre(string.Empty)
				.WithSegundoNombre(textWith60Charters)
				.WithApellido(string.Empty)
				.WithSegundoApellido(textWith60Charters)
				.WithEdad(5);

			// Act
			var exception = Assert.Throws<ValidationException>(() =>
			{
				clienteBuilder.BuildToUpdate();
			});

			// Assert
			Assert.Equal(expectedUpdateValidationExceptionMessage, exception.Message);
			Assert.True(exception.Errors.Length == expectedNumValidationErrors);
			Assert.Collection(exception.Errors,
				error => Assert.Equal(expectedNombreValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedNombreRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedSegundoNombreRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedApellidoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedApellidoRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedSegundoApellidoRangoValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedEdadlValidationErrorMessage, error.Message),
				error => Assert.Equal(expectedIdValidationErrorMessage, error.Message)
			);
		}
	}
}
