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
                .When(x => x.StatusId == BulletinConstants.Status.NewEISS || x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.DecisionNumber)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.DecisionDate)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.DecisionFinalDate)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.DecidingAuthId)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(50);

            RuleFor(x => x.CaseNumber)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.CaseYear)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.AlphabeticalIndex)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewEISS || x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.BulletinCreateDate)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.CreatedByNames)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.ApprovedByNames)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.ApprovedByPosition)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.Firstname)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(200);

            RuleFor(x => x.Surname)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(200);

            RuleFor(x => x.Familyname)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(200);

            RuleFor(x => x.FirstnameLat)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.SurnameLat)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(200);

            RuleFor(x => x.FamilynameLat)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(200);

            RuleFor(x => x.Sex)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

            RuleFor(x => x.Egn)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.Ln)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.Lnch)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice)
                .HasMaxLength(100);

            RuleFor(x => x.BirthDate)
                .RequredField()
                .When(x => x.StatusId == BulletinConstants.Status.NewOffice);

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
        }
    }
}
