using FluentValidation;
using MJ_CAIS.Common.Resources;

namespace MJ_CAIS.FluentValidators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> RequredField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(FluentValidationResources.NotEmpty);
        }

        public static IRuleBuilderOptions<T, string?> HasMaxLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
        {
            var message = string.Format(FluentValidationResources.HasMaxLength, maxLength);
            return ruleBuilder
                .Length(0, 100)
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<T, string?> HasMinLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLength)
        {
            var message = string.Format(FluentValidationResources.HasMinLength, minLength);
            return ruleBuilder
                .Length(minLength, int.MaxValue)
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<T, string?> HasLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLength, int maxLength)
        {
            var message = string.Format(FluentValidationResources.HasLength, maxLength, minLength);
            return ruleBuilder
                .Length(minLength, maxLength)
                .WithMessage(message);
        }
    }
}