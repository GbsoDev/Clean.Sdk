using Clean.Sdk.Data.EfCore.Entities;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;

namespace Clean.Sdk.Data.EfCore.Tests;

/// <summary>
/// Entity representation of <see cref="ClientTest"/> for Entity Framework Core persistence.
/// This class should only be instantiated by EF Core during database hydration.
/// For creating new clients in application code, use <see cref="ClientTest"/> constructors.
/// </summary>
public class ClientTestEntity : ClientTest, IDomainEntity
{
    object IDomainModel.Id => Id;

    /// <summary>
    /// Private parameterless constructor used by Entity Framework Core for entity hydration.
    /// EF Core uses reflection to invoke this constructor when materializing entities from the database.
    /// ⚠️ DO NOT use directly in application code.
    /// </summary>
#pragma warning disable CS8618
    private ClientTestEntity() : base()
    {
    }
#pragma warning restore CS8618
}

