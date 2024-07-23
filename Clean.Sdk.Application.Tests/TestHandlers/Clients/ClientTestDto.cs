namespace Clean.Sdk.Application.Tests.TestHandlers.Clients
{
	public record ClientTestDto
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string? MiddleName { get; private set; }
		public string Surname { get; private set; }
		public short Age { get; private set; }

		public ClientTestDto(string name, string? middleName, string surname, short age)
			: this(Guid.Empty, name, middleName, surname, age)
		{
		}

		public ClientTestDto(Guid id, string name, string? middleName, string surname, short age)
		{
			Id = id;
			Name = name;
			MiddleName = middleName;
			Surname = surname;
			Age = age;
		}
	}
}
