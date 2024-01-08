using System;

namespace Clean.Domain.Entity.Validations
{
	public class EntityValidationException
		: Exception
	{
		public readonly EntityValidationResult _validationResult;

		public EntityValidationError[] Errors => _validationResult.Errors.ToArray();

		public EntityValidationException(string message)
			: base(message)
		{
			_validationResult = new EntityValidationResult();
			_validationResult.AddError(message);
		}

		public EntityValidationException(EntityValidationResult validation, string message)
			: base(message)
		{
			_validationResult = validation;
		}
	}
}
