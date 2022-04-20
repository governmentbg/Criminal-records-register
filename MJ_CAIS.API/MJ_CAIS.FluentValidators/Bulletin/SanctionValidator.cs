using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class SanctionValidator : AbstractValidator<SanctionDTO>
    {
        public SanctionValidator()
        {
            RuleFor(x => x.SanctCategoryId)
                .NotEmpty()
                .HasMaxLength(50);

            //RuleFor(x => x.FineAmount).NotEmpty(); // todo: if Fine is selected
            RuleFor(x => x.EcrisSanctCategId).HasMaxLength(50);
        }
    }
}