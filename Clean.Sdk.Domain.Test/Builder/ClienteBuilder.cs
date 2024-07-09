using Clean.Sdk.Domain.Test.TestModel.Clientes;

namespace Clean.Sdk.Domain.Tests.Builders
{
	public sealed class ClienteBuilder
	{
		private Guid _id;
		private string _nombre;
		private string? _segundoNombre;
		private string _apellido;
		private string? _segundoApellido;
		private short _edad;

		public ClienteBuilder()
		{
			_id = Guid.NewGuid();
			_nombre = "Nombre";
			_segundoNombre = null;
			_apellido = "Apellido";
			_segundoApellido = null;
			_edad = 30;
		}

		public ClienteBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public ClienteBuilder WithNombre(string nombre)
		{
			_nombre = nombre;
			return this;
		}

		public ClienteBuilder WithSegundoNombre(string? segundoNombre)
		{
			_segundoNombre = segundoNombre;
			return this;
		}

		public ClienteBuilder WithApellido(string apellido)
		{
			_apellido = apellido;
			return this;
		}

		public ClienteBuilder WithSegundoApellido(string? segundoApellido)
		{
			_segundoApellido = segundoApellido;
			return this;
		}

		public ClienteBuilder WithEdad(short edad)
		{
			_edad = edad;
			return this;
		}

		public Cliente BuildToCreate()
		{
			return new Cliente(_nombre, _segundoNombre, _apellido, _segundoApellido, _edad);
		}

		public Cliente BuildToUpdate()
		{
			return new Cliente(_id, _nombre, _segundoNombre, _apellido, _segundoApellido, _edad);
		}
	}
}
