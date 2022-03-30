using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinValidator : AbstractValidator<BulletinDTO>
    {
        public BulletinValidator()
        {
            RuleFor(x => x.SequentialIndex).RequredField();

            RuleFor(x => x.DecisionNumber)
                .HasMaxLength(100)
                .RequredField();

            RuleFor(x => x.DecisionDate).RequredField();
            RuleFor(x => x.DecisionFinalDate).RequredField();

            RuleFor(x => x.DecidingAuthId)
                .RequredField()
                .HasMaxLength(50);

            RuleFor(x => x.CaseNumber)
                .RequredField()
                .HasMaxLength(100);

            RuleFor(x => x.CaseYear).RequredField();

            RuleFor(x => x.AlphabeticalIndex)
                .RequredField()
                .HasMaxLength(100);

            RuleFor(x => x.BulletinCreateDate).RequredField();
            RuleFor(x => x.CreatedByNames).RequredField();
            RuleFor(x => x.ApprovedByNames).RequredField();
            RuleFor(x => x.ApprovedByPosition).RequredField();

            RuleFor(x => x.Firstname)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.Surname)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.Familyname)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.FirstnameLat)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.SurnameLat)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.FamilynameLat)
                .RequredField()
                .HasMaxLength(200);

            RuleFor(x => x.Sex).RequredField();
            RuleFor(x => x.Egn)
                .RequredField()
                .HasMaxLength(100);

            RuleFor(x => x.Ln)
                .RequredField()
                .HasMaxLength(100);

            RuleFor(x => x.Lnch)
                .RequredField()
                .HasMaxLength(100);

            RuleFor(x => x.BirthDate).RequredField();
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