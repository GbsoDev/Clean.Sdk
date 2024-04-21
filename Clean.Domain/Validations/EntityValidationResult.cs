using System.Collections.Generic;
using System.Linq;

namespace Clean.Domain.Validations
{
	public class EntityValidationResult
	{
		public bool IsValid => !Errors.Any();
		public List<EntityValidationError> Errors { get; }

		public EntityValidationResult()
		{
			Errors = new List<EntityValidationError>();
		}

		public void AddError(string message)
		{
			Errors.Add(new EntityValidationError(message));
		}
	}
}
