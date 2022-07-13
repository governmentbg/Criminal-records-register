using FluentValidation;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.Common.Validators;
using MJ_CAIS.DTO.User;

namespace MJ_CAIS.FluentValidators.User
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Firstname)
                .RequredField();

            RuleFor(x => x.Surname)
                .RequredField();

            RuleFor(x => x.Familyname)
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
