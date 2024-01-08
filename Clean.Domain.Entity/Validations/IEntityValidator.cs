namespace Clean.Domain.Entity.Validations
{
    public interface IEntityValidator<out TEntity>
		where TEntity : IDomainEntity
	{
		EntityValidationResult ValidateToCreate(IDomainEntity entity);
		void ValidateToCreateThrow(IDomainEntity entity);
		EntityValidationResult ValidateToUpdate(IDomainEntity entity);
		void ValidateToUpdateThrow(IDomainEntity entity);
	}
}
