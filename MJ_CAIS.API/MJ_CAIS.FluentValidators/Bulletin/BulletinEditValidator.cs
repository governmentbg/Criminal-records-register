using FluentValidation;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinEditValidator : AbstractValidator<BulletinEditDTO>
    {
        public BulletinEditValidator()
        {
            RuleFor(x => x.DecisionNumber)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.DecisionDate)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.DecisionFinalDate)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.CaseAuthId)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.CaseNumber)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.CaseYear)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.AlphabeticalIndex)
                .HasMaxLength(100);

            RuleFor(x => x.BulletinAuthorityId).HasMaxLength(50);
            RuleFor(x => x.BulletinType).HasMaxLength(50);
            RuleFor(x => x.CaseTypeId).HasMaxLength(50);
            RuleFor(x => x.CreatedByPosition).HasMaxLength(200);
            RuleFor(x => x.DecisionEcli).HasMaxLength(100);
            RuleFor(x => x.DecisionTypeId).HasMaxLength(50);
            RuleFor(x => x.EcrisConvictionId).HasMaxLength(50);
            RuleFor(x => x.StatusId).HasMaxLength(50);

            //RuleFor(x => x.DecisionsTransactions)
            //    .MustBeEmptyWhenIsInStatus("Допълнителни сведения",
            //                        BulletinConstants.Status.Active,
            //                        BulletinConstants.Status.ForRehabilitation);

            //RuleFor(x => x.OffancesTransactions)
            //    .MustBeEmptyWhenIsInStatus("Престъпления", BulletinConstants.Status.NewOffice);

            //RuleFor(x => x.SanctionsTransactions)
            //  .MustBeEmptyWhenIsInStatus("Наказания", BulletinConstants.Status.NewOffice);
        }
    }
}
