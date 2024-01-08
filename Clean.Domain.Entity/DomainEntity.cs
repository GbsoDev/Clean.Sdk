using Clean.Domain.Entity;
using Clean.Domain.Entity.Validations;
using System;

namespace Maios.CRM.Domain.Entity
{
    public abstract class DomainEntity<TId> : IDomainEntity
		where TId : struct
	{
		public TId Id { get; protected set; }

		object IDomainEntity.Id => Id;

		protected Action<IDomainEntity> ValidateAction { get; }

		protected DomainEntity(Action<IDomainEntity> validateAction)
		{
			ValidateAction = validateAction;
		}
	}
}
namespace Maios.CRM.Domain.Entity
{
    public abstract class DomainEntity<TId, TValidator> : DomainEntity<TId>
		where TId : struct
		where TValidator : IEntityValidator<IDomainEntity>, new()
	{
		protected DomainEntity(Action<IDomainEntity> validateAction)
			: base(validateAction)
		{
		}

		protected static void ValidateToCreateThrow(IDomainEntity entity)
		{
			var Validator = new TValidator();
			Validator.ValidateToCreateThrow(entity);
		}

		protected static void ValidateToUpdateThrow(IDomainEntity entity)
		{
			var Validator = new TValidator();
			Validator.ValidateToUpdateThrow(entity);
		}
	}
}
