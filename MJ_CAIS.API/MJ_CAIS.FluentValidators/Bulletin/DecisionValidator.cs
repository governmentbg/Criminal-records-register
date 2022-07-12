using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class DecisionValidator : AbstractValidator<DecisionDTO>
    {
        public DecisionValidator()
        {
            RuleFor(x => x.DecisionAuthId).HasMaxLength(50);
            RuleFor(x => x.DecisionChTypeId).HasMaxLength(50);
            RuleFor(x => x.DecisionEcli).HasMaxLength(100);
            RuleFor(x => x.DecisionNumber).HasMaxLength(100);
            RuleFor(x => x.DecisionTypeId).HasMaxLength(50);
        }
    }
}