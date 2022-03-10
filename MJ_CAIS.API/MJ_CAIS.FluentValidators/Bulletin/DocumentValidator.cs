using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class DocumentValidator : AbstractValidator<DocumentDTO>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.Descr).NotEmpty();
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please specify a first name");
            RuleFor(x => x.MimeType).Must(BeAValidPostcode).WithMessage("Please specify a valid mime type");
        }

        private bool BeAValidPostcode(string postcode)
        {
            // custom postcode validating logic goes here
            return true;
        }
    }
}
