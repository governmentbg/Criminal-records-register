using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class SanctionValidator : AbstractValidator<SanctionDTO>
    {
        public SanctionValidator()
        {
            RuleFor(x => x.SanctCategoryId).NotEmpty();
            RuleFor(x => x.FineAmount).NotEmpty();
            RuleFor(x => x.ProbationDescr).NotEmpty();
        }
    }
}