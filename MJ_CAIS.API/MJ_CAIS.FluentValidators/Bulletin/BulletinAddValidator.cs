using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinAddValidator : AbstractValidator<BulletinAddDTO>
    {
        public BulletinAddValidator()
        {
            //RuleFor(x => x.DecisionNumber)
            //    .HasMaxLength(100)
            //    .RequredField();

            //RuleFor(x => x.DecisionDate).RequredField();
            //RuleFor(x => x.DecisionFinalDate).RequredField();

            //RuleFor(x => x.DecidingAuthId)
            //    .RequredField()
            //    .HasMaxLength(50);

            //RuleFor(x => x.CaseNumber)
            //    .RequredField()
            //    .HasMaxLength(100);

            //RuleFor(x => x.CaseYear).RequredField();

            //RuleFor(x => x.AlphabeticalIndex)
            //    .HasMaxLength(100);

            //RuleFor(x => x.BulletinCreateDate).RequredField();

            //RuleFor(x => x.BulletinAuthorityId).HasMaxLength(50);
            //RuleFor(x => x.BulletinType).HasMaxLength(50);
            //RuleFor(x => x.CaseTypeId).HasMaxLength(50);
            //RuleFor(x => x.CreatedByPosition).HasMaxLength(200);
            //RuleFor(x => x.DecisionEcli).HasMaxLength(100);
            //RuleFor(x => x.DecisionTypeId).HasMaxLength(50);
            //RuleFor(x => x.EcrisConvictionId).HasMaxLength(50);
            //RuleFor(x => x.StatusId).HasMaxLength(50);
        }
    }
}