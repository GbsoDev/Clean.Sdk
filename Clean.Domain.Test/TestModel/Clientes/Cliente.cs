using Clean.Domain.Entity;
using Clean.Domain.Validations;

namespace Clean.Domain.Test.TestModel.Clientes
{
	public class Cliente : DomainEntity<Guid>, IAuditableEntity
	{
		public string Nombre { get; private set; }
		public string? SegundoNombre { get; private set; }
		public string Apellido { get; private set; }
		public string? SegundoApellido { get; private set; }
		public short Edad { get; private set; }

		public Cliente(string nombre, string? segundoNombre, string apellido, string? segundoApellido, short edad)
			: this(Guid.Empty, nombre, segundoNombre, apellido, segundoApellido, edad, ClienteValidator.ValidateToCreate)
		{
		}

		public Cliente(Guid id, string nombre, string? segundoNombre, string apellido, string? segundoApellido, short edad)
			: this(id, nombre, segundoNombre, apellido, segundoApellido, edad, ClienteValidator.ValidateToUpdate)
		{
		}

		private Cliente(Guid id, string nombre, string? segundoNombre, string apellido, string? segundoApellido, short edad, Func<Cliente, ValidationSet> validateAction)
		{
			Id = id;
			Nombre = nombre.Trim();
			SegundoNombre = segundoNombre?.Trim();
			Apellido = apellido.Trim();
			SegundoApellido = segundoApellido?.Trim();
			Edad = edad;
			validateAction.Invoke(this).ValidateAndThrow();
		}
	}
}
