using FluentValidation;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.FluentValidators.Bulletin
{
    public static class BulletinRuleExtensions
    {
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

        /// <summary>
        /// Допълнителни сведения => могат да се променят ако бюлетина е отключен или
        /// в статус Активен или Подлежащ на реабилитация
        /// Останалите колекции => могат да се редактират всички отключени
        /// или тези в статус нови добавени от БС
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="listName">Име на колекцията</param>
        /// <param name="allowedStatuses">Статуси за които е позволено да има редакция на колекцията</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<BulletinEditDTO, IList<TElement>> MustBeEmptyIfWhenIsInStatus<TElement>(this IRuleBuilder<BulletinEditDTO, IList<TElement>> ruleBuilder, string listName, params string[] allowedStatuses)
        {
            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter
                    .AppendArgument("ObjName", listName);

                if (list == null || list.Count == 0) return true;

                var isLocked = rootObject.Locked.HasValue && rootObject.Locked.Value;
                var result = !isLocked || allowedStatuses.Any(x=> rootObject.StatusId == x);
                return result;
            })
            .WithMessage("Нямате права да променяте '{ObjName}' към бюлетин в този статус");
        }
    }
}