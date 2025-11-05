using FluentValidation;

namespace Common.Application.Extensions.FluentValidation
{
    public static class ForEachSetValidatorValidator
    {
        public static IRuleBuilderOptions<T, IEnumerable<TElement>> ForEachSetValidator<T, TElement>(this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder, AbstractValidator<TElement> validator)
            => ruleBuilder.ForEach(list => list.SetValidator(validator));
    }
}
