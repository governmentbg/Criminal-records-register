using FluentValidation;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinEditValidator : AbstractValidator<BulletinEditDTO>
    {
        public BulletinEditValidator()
        {
            RuleFor(x => x.SequentialIndex)
                .RequredField()
                .WhenBulletinIsUnlockedNewEISS();

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

            RuleFor(x => x.DecidingAuthId)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(50);

            RuleFor(x => x.CaseNumber)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.CaseYear)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.AlphabeticalIndex)
                .RequredField()
                .WhenBulletinIsUnlockedNewEISS()
                .HasMaxLength(100);

            RuleFor(x => x.BulletinCreateDate)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.CreatedByNames)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.ApprovedByNames)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.ApprovedByPosition)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.Firstname)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.Surname)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.Familyname)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.FirstnameLat)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.SurnameLat)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.FamilynameLat)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(200);

            RuleFor(x => x.Sex)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.Egn)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.Ln)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.Lnch)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice()
                .HasMaxLength(100);

            RuleFor(x => x.BirthDate)
                .RequredField()
                .WhenBulletinIsUnlockedNewOffice();

            RuleFor(x => x.AfisNumber).HasMaxLength(100);
            RuleFor(x => x.BirthDatePrecision).HasMaxLength(200);
            RuleFor(x => x.BulletinAuthorityId).HasMaxLength(50);
            RuleFor(x => x.BulletinType).HasMaxLength(50);
            RuleFor(x => x.CaseTypeId).HasMaxLength(50);
            RuleFor(x => x.CreatedByPosition).HasMaxLength(200);
            RuleFor(x => x.DecisionEcli).HasMaxLength(100);
            RuleFor(x => x.DecisionTypeId).HasMaxLength(50);
            RuleFor(x => x.EcrisConvictionId).HasMaxLength(50);
            RuleFor(x => x.FatherFamilyname).HasMaxLength(200);
            RuleFor(x => x.FatherFirstname).HasMaxLength(200);
            RuleFor(x => x.FatherFullname).HasMaxLength(200);
            RuleFor(x => x.Fullname).HasMaxLength(200);
            RuleFor(x => x.FullnameLat).HasMaxLength(200);
            RuleFor(x => x.IdDocCategoryId).HasMaxLength(50);
            RuleFor(x => x.IdDocIssuingDatePrec).HasMaxLength(200);
            RuleFor(x => x.IdDocNumber).HasMaxLength(100);
            RuleFor(x => x.IdDocValidDatePrec).HasMaxLength(200);
            RuleFor(x => x.MotherFamilyname).HasMaxLength(200);
            RuleFor(x => x.MotherFirstname).HasMaxLength(200);
            RuleFor(x => x.MotherFullname).HasMaxLength(200);
            RuleFor(x => x.MotherSurname).HasMaxLength(200);
            RuleFor(x => x.RegistrationNumber).HasMaxLength(100);
            RuleFor(x => x.StatusId).HasMaxLength(50);
            RuleFor(x => x.Surname).HasMaxLength(200);

            RuleFor(x => x.DecisionsTransactions)
                .MustBeEmptyIfWhenIsInStatus("Допълнителни сведения",
                                    BulletinConstants.Status.Active,
                                    BulletinConstants.Status.ForRehabilitation);

            RuleFor(x => x.OffancesTransactions)
                .MustBeEmptyIfWhenIsInStatus("Престъпления", BulletinConstants.Status.NewOffice);

              RuleFor(x => x.SanctionsTransactions)
                .MustBeEmptyIfWhenIsInStatus("Наказания", BulletinConstants.Status.NewOffice);
        }
    }
}
