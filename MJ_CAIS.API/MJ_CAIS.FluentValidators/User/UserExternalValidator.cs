using FluentValidation;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.Validators;
using MJ_CAIS.DTO.UserExternal;

namespace MJ_CAIS.FluentValidators.User
{
    public class UserExternalValidator : AbstractValidator<UserExternalDTO>
    {
        public UserExternalValidator()
        {
            RuleFor(x => x.Name)
               .RequredField();

            RuleFor(x => x.AdministrationId)
              .RequredField();

            RuleFor(x => x.Egn)
                .Custom((egn, context) =>
                {
                    if (string.IsNullOrEmpty(egn))
                    {
                        context.AddFailure(FluentValidationResources.NotEmpty);
                    }
                
                    if (!EgnValidator.IsValid(egn))
                    {
                        context.AddFailure(FluentValidationResources.InvalidEgn);
                    }
                });

            RuleFor(x => x.Email)
              .Custom((email, context) =>
              {
                  if (!EmailAddressValidator.IsValid(email))
                  {
                      context.AddFailure(FluentValidationResources.InvalidEmail);
                  }
              });
        }
    }
}
