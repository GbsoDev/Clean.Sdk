using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;
using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.TestModel.ClientsTest
{
    public class ClientTest : IDomainModel
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string? MiddleName { get; protected set; }
        public string Surname { get; protected set; }
        public short Age { get; protected set; }

        object IDomainModel.Id => Id;

        /// <summary>
        /// Creates a new instance of <see cref="ClientTest"/> with creation validations.
        /// The Id will be generated automatically.
        /// </summary>
        /// <param name="name">Client name (will be normalized with Trim)</param>
        /// <param name="middleName">Client middle name (optional, will be normalized with Trim)</param>
        /// <param name="surname">Client surname (will be normalized with Trim)</param>
        /// <param name="age">Client age</param>
        /// <exception cref="ValidationException">Thrown if creation validations fail</exception>
        public ClientTest(string name, string? middleName, string surname, short age)
            : this(Guid.Empty, name, middleName, surname, age, ClientTestValidator.ValidateToCreate)
        {
        }

        /// <summary>
        /// Creates a <see cref="ClientTest"/> instance with a specific Id and update validations.
        /// Use when updating or reconstructing an existing client that must pass validations.
        /// </summary>
        /// <param name="id">Unique client identifier</param>
        /// <param name="name">Client name (will be normalized with Trim)</param>
        /// <param name="middleName">Client middle name (optional, will be normalized with Trim)</param>
        /// <param name="surname">Client surname (will be normalized with Trim)</param>
        /// <param name="age">Client age</param>
        /// <exception cref="ValidationException">Thrown if update validations fail</exception>
        public ClientTest(Guid id, string name, string? middleName, string surname, short age)
            : this(id, name, middleName, surname, age, ClientTestValidator.ValidateToUpdate)
        {
        }

        /// <summary>
        /// Private base constructor that executes the common initialization and validation logic.
        /// </summary>
        private ClientTest(Guid id, string name, string? middleName, string surname, short age, Func<ClientTest, ValidationSet> validateAction)
        {
            Id = id;
            Name = name.Trim();
            MiddleName = middleName?.Trim();
            Surname = surname.Trim();
            Age = age;
            validateAction.Invoke(this).ValidateAndThrow();
        }

        /// <summary>
        /// Protected parameterless constructor to allow inheritance.
        /// Used by Entity Framework Core in derived classes for entity hydration via reflection.
        /// ⚠️ EXCLUSIVE USE for derived classes in the persistence layer.
        /// </summary>
#pragma warning disable CS8618
        protected ClientTest()
        {
        }
#pragma warning restore CS8618
    }
}
