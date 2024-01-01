namespace Clean.Domain.Entity.Validations
{
    public abstract class EntityValidator<TEntity> : IEntityValidator<TEntity>
		where TEntity : IDomainEntity
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
			if (ValidationResult.IsValid)
				throw new EntityValidationException(ValidationResult, string.Format(EntityValidationMessages.InvalidEntityToCreate, entity.GetType().Name));
		}

		public abstract EntityValidationResult ValidateToUpdate(TEntity entity);

		public virtual void ValidateToUpdateThrow(TEntity entity)
		{
			ValidateToUpdate(entity);
			if (ValidationResult.IsValid)
				throw new EntityValidationException(ValidationResult, string.Format(EntityValidationMessages.InvalidEntityToUpdate, entity.GetType().Name));
		}

		public EntityValidationResult ValidateToCreate(IDomainEntity entity) => ValidateToCreate((TEntity)entity);

		public void ValidateToCreateThrow(IDomainEntity entity) => ValidateToCreateThrow((TEntity)entity);

		public EntityValidationResult ValidateToUpdate(IDomainEntity entity) => ValidateToUpdate((TEntity)entity);

		public void ValidateToUpdateThrow(IDomainEntity entity) => ValidateToUpdateThrow((TEntity)entity);
	}
}
