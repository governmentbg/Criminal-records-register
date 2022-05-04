using FluentValidation;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.FluentValidators.Person
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator()
        {
            // todo: add validation 
            // context type (bulletin, fbbc, application or person)
            // if is in bulletin check for bulletin status ??
        }
    }
}
