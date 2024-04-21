using Clean.Domain.Entity;

namespace Clean.Domain.Validations
{
	public abstract class EntityValidator<TEntity>
		where TEntity : class, IDomainEntity
	{
		protected EntityValidationResult ValidationResult { get; }

		protected EntityValidator()
		{
			ValidationResult = new EntityValidationResult();
		}

		public abstract EntityValidationResult ValidateToCreate(TEntity entity);

		public virtual void ValidateToCreateThrow(TEntity entity)
		{
			ValidateToCreate(entity);
			if (!ValidationResult.IsValid)
				throw new EntityValidationException(ValidationResult, string.Format(EntityValidationMessages.InvalidEntityToCreate, entity.GetType().Name));
		}

		public abstract EntityValidationResult ValidateToUpdate(TEntity entity);

		public virtual void ValidateToUpdateThrow(TEntity entity)
		{
			ValidateToUpdate(entity);
			if (!ValidationResult.IsValid)
				throw new EntityValidationException(ValidationResult, string.Format(EntityValidationMessages.InvalidEntityToUpdate, entity.GetType().Name));
		}
	}
}
