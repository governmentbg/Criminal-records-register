using FluentValidation;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> RequredField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(ValidationMessageConstants.NotEmpty);
        }

        public static IRuleBuilderOptions<T, string?> HasMaxLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
        {
            var message = string.Format(ValidationMessageConstants.HasMaxLength, maxLength);
            return ruleBuilder
                .Length(0, 100)
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<T, string?> HasMinLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLength)
        {
            var message = string.Format(ValidationMessageConstants.HasMinLength, minLength);
            return ruleBuilder
                .Length(minLength, int.MaxValue)
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<T, string?> HasLength<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLength, int maxLength)
        {
            var message = string.Format(ValidationMessageConstants.HasLength, maxLength, minLength);
            return ruleBuilder
                .Length(minLength, maxLength)
                .WithMessage(message);
        }

        public static IRuleBuilderOptions<BulletinEditDTO, TProperty> WhenBulletinIsUnlockedNewOffice<TProperty>(this IRuleBuilderOptions<BulletinEditDTO, TProperty> rule)
        {
            rule.When(x => x.StatusId == BulletinConstants.Status.NewOffice || (x.Locked.HasValue && !x.Locked.Value));
            return rule;
        }

        public static IRuleBuilderOptions<BulletinEditDTO, TProperty> WhenBulletinIsUnlockedNewEISS<TProperty>(this IRuleBuilderOptions<BulletinEditDTO, TProperty> rule)
        {
            rule.When(x => x.StatusId == BulletinConstants.Status.NewEISS ||
                           x.StatusId == BulletinConstants.Status.NewOffice ||
                            (x.Locked.HasValue && !x.Locked.Value));
            return rule;
        }
    }
}