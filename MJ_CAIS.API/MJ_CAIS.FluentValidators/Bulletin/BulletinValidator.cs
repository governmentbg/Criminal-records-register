using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinValidator : AbstractValidator<BulletinDTO>
    {
        public BulletinValidator()
        {
            RuleFor(x => x.SequentialIndex).NotEmpty();
            RuleFor(x => x.DecisionNumber).NotEmpty();
            RuleFor(x => x.DecisionDate).NotEmpty();
            RuleFor(x => x.DecisionFinalDate).NotEmpty();
            RuleFor(x => x.DecidingAuthId).NotEmpty();
            RuleFor(x => x.CaseNumber).NotEmpty();
            RuleFor(x => x.CaseYear).NotEmpty();
            RuleFor(x => x.AlphabeticalIndex).NotEmpty();
            RuleFor(x => x.BulletinCreateDate).NotEmpty();
            RuleFor(x => x.CreatedByNames).NotEmpty();
            RuleFor(x => x.ApprovedByNames).NotEmpty();
            RuleFor(x => x.ApprovedByPosition).NotEmpty();
            //RuleFor(x => x.StatusId).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Familyname).NotEmpty();
            RuleFor(x => x.FirstnameLat).NotEmpty();
            RuleFor(x => x.SurnameLat).NotEmpty();
            RuleFor(x => x.FamilynameLat).NotEmpty();
            RuleFor(x => x.Sex).NotEmpty();
            RuleFor(x => x.Egn).NotEmpty();
            RuleFor(x => x.Ln).NotEmpty();
            RuleFor(x => x.Lnch).NotEmpty();
            RuleFor(x => x.BirthDate).NotEmpty();
            //RuleFor(x => x.BirthCityId).NotEmpty();
            //RuleFor(x => x.BirthCountryId).NotEmpty();
        }
    }
}