using FluentValidation;
using System.Text.RegularExpressions;

public static class AcceptLanguageValidator
{
    const string ArLangPattern = @"^[\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s\p{P}]+$";
    const string EnLangPattern = @"^[a-zA-Z\s\p{P}]+$";

    const string ArLangCombineNumsPattern = @"^[\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s\d\p{P}]+$";
    const string EnLangCombineNumsPattern = @"^[a-zA-Z\s\d\p{P}]+$";

    public static IRuleBuilderOptions<T, string> IsArabic<T>(
        this IRuleBuilder<T, string> ruleBuilder, bool combineNumbers = false)
    {
        return ruleBuilder.Must(x => VerifyArabicLanguage(x, combineNumbers))
                          .WithMessage("The text must be in Arabic and match the specified criteria.");
    }

    public static IRuleBuilderOptions<T, string> IsEnglish<T>(
        this IRuleBuilder<T, string> ruleBuilder, bool combineNumbers = false)
    {
        return ruleBuilder.Must(x => VerifyEnglishLanguage(x, combineNumbers))
                          .WithMessage("The text must be in English and match the specified criteria.");
    }

    private static bool VerifyArabicLanguage(string name, bool combineNumbers = false)
    {
        if (combineNumbers)
            return Regex.IsMatch(name, ArLangCombineNumsPattern);

        return Regex.IsMatch(name, ArLangPattern);
    }

    private static bool VerifyEnglishLanguage(string name, bool combineNumbers = false)
    {
        if (combineNumbers)
            return Regex.IsMatch(name, EnLangCombineNumsPattern);

        return Regex.IsMatch(name, EnLangPattern);
    }
}