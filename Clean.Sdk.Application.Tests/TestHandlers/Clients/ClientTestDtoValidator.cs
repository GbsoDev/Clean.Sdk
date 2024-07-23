using Clean.Sdk.Application.Validations;
using Clean.Sdk.Domain.Tests.TestEntites;
using Clean.Sdk.Domain.Tests.TestEntites.Clients;
using FluentValidation;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients
{
	public class ClientTestDtoValidator<TCommand> : AbstractValidator<TCommand>
		where TCommand : ClientTestDto
	{
		public ClientTestDtoValidator()
		{
			RuleSet(ValidationsSet.CREATION, () =>
			{
				RuleFor(clientDto => clientDto.Name)
					.NotEmpty().WithMessage(clientDto => string.Format(ValidationErrorMessages.Required, nameof(clientDto.Name)))
					.Length(ClientParameters.NameMinLength, ClientParameters.NameMaxLength)
					.WithMessage(clientDto => string.Format(ValidationErrorMessages.Range, nameof(clientDto.Name), ClientParameters.NameMinLength, ClientParameters.NameMaxLength));

				RuleFor(clientDto => clientDto.MiddleName)
					.Length(ClientParameters.NameMinLength, ClientParameters.NameMaxLength)
					.WithMessage(clientDto => string.Format(ValidationErrorMessages.Range, nameof(clientDto.MiddleName), ClientParameters.NameMinLength, ClientParameters.NameMaxLength))
					.When(clientDto => clientDto.MiddleName != null);

				RuleFor(clientDto => clientDto.Surname)
					.NotEmpty().WithMessage(clientDto => string.Format(ValidationErrorMessages.Required, nameof(clientDto.Surname)))
					.Length(ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength)
					.WithMessage(clientDto => string.Format(ValidationErrorMessages.Range, nameof(clientDto.Surname), ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength));

				RuleFor(clientDto => clientDto.Age)
					.GreaterThanOrEqualTo(ClientParameters.EageMin).WithMessage(clientDto => string.Format(ValidationErrorMessages.MinimumAge, nameof(clientDto.Age)));
			});

			RuleSet(ValidationsSet.UPDATE, () =>
			{
				RuleFor(clientDto => clientDto.Id)
					.NotEqual(Guid.Empty).WithMessage(clientDto => string.Format(ValidationErrorMessages.Required, nameof(clientDto.Id)));
				RuleFor(clientDto => clientDto)
					.SetValidator(this, ruleSets: ValidationsSet.CREATION);
			});
		}
	}
}
