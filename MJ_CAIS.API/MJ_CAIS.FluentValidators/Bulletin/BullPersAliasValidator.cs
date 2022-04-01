using FluentValidation;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BullPersAliasValidator : AbstractValidator<PersonAliasDTO>
    {
        public BullPersAliasValidator()
        {
            RuleFor(x => x.Familyname).HasMaxLength(200);
            RuleFor(x => x.Firstname).HasMaxLength(200);
            RuleFor(x => x.Fullname).HasMaxLength(200);
            RuleFor(x => x.Surname).HasMaxLength(200);
            RuleFor(x => x.TypeCode).HasMaxLength(200);
        }
    }
}