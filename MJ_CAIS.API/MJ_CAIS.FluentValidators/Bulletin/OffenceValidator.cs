using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class OffenceValidator : AbstractValidator<OffenceDTO>
    {
        public OffenceValidator()
        {
            RuleFor(x => x.OffenceCategory.Id).NotEmpty();
            RuleFor(x => x.Remarks).NotEmpty();
        }
    }
}