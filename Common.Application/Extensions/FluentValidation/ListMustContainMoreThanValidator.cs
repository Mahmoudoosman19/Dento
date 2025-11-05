using FluentValidation;

namespace Common.Application.Extensions.FluentValidation
{
    public static class ListMustContainMoreThanValidator
    {
        public static IRuleBuilderOptions<T, IEnumerable<TElement>> ListMustContainMoreThan<T, TElement>(this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder, int count = 0)
            => ruleBuilder.Must(list => list.Count() > count);
    }
}
