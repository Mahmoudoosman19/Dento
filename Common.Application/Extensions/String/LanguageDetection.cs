using System.Text.RegularExpressions;

namespace Common.Application.Extensions.String
{
    public static class LanguageDetection
    {
        const string ArLangPattern = @"^[\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s\p{P}]+$";
        const string EnLangPattern = @"^[a-zA-Z\s\p{P}]+$";

        const string ArLangCombineNumsPattern = @"^[\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s\d\p{P}]+$";
        const string EnLangCombineNumsPattern = @"^[a-zA-Z\s\d\p{P}]+$";

        public static bool IsArabicLanguage(this string text)
        {
            return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, ArLangPattern);
        }

        public static bool IsArabicLanguageCombinedByNumbers(this string text)
        {
            return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, ArLangCombineNumsPattern);
        }

        public static bool IsEnglishLanguage(this string text)
        {
            return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, EnLangPattern);
        }
        public static bool IsEnglishLanguageCombinedByNumbers(this string text)
        {
            return !string.IsNullOrEmpty(text) && Regex.IsMatch(text, EnLangCombineNumsPattern);
        }
    }
}
