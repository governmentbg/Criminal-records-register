using FluentValidation;

namespace MJ_CAIS.FluentValidators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> RequredField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().WithMessage(ValidationMessageConstants.NotEmpty);
        }
    }
}
