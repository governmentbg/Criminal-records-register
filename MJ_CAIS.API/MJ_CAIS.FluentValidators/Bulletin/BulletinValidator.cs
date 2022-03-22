using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class BulletinValidator : AbstractValidator<BulletinDTO>
    {
        public BulletinValidator()
        {
            RuleFor(x => x.SequentialIndex).RequredField();
            RuleFor(x => x.DecisionNumber).RequredField();
            RuleFor(x => x.DecisionDate).RequredField();
            RuleFor(x => x.DecisionFinalDate).RequredField();
            RuleFor(x => x.DecidingAuthId).RequredField();
            RuleFor(x => x.CaseNumber).RequredField();
            RuleFor(x => x.CaseYear).RequredField();
            RuleFor(x => x.AlphabeticalIndex).RequredField();
            RuleFor(x => x.BulletinCreateDate).RequredField();
            RuleFor(x => x.CreatedByNames).RequredField();
            RuleFor(x => x.ApprovedByNames).RequredField();
            RuleFor(x => x.ApprovedByPosition).RequredField();
            //RuleFor(x => x.StatusId).RequredField();
            RuleFor(x => x.Firstname).RequredField();
            RuleFor(x => x.Surname).RequredField();
            RuleFor(x => x.Familyname).RequredField();
            RuleFor(x => x.FirstnameLat).RequredField();
            RuleFor(x => x.SurnameLat).RequredField();
            RuleFor(x => x.FamilynameLat).RequredField();
            RuleFor(x => x.Sex).RequredField();
            RuleFor(x => x.Egn).RequredField();
            RuleFor(x => x.Ln).RequredField();
            RuleFor(x => x.Lnch).RequredField();
            RuleFor(x => x.BirthDate).RequredField();
            //RuleFor(x => x.BirthCityId).RequredField();
            //RuleFor(x => x.BirthCountryId).RequredField();
        }
    }
}