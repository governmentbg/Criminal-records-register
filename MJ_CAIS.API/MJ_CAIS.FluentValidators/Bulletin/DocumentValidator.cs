using FluentValidation;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public class DocumentValidator : AbstractValidator<DocumentDTO>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.DocTypeId).HasMaxLength(50);
            RuleFor(x => x.Name).HasMaxLength(200);
        }
    }
}