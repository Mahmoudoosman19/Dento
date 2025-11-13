using Microsoft.AspNetCore.Http;

namespace Common.Application.Localization
{
    public class Localizer: ILocalizer
    {
        public string GetLanguage()
        {
            var httpContext = GetHttpContext(); 

            string acceptLanguage = httpContext.Request.Headers["Accept-Language"]!;

            string language = acceptLanguage?.Split(',').FirstOrDefault()?.Trim()?.Split(';').FirstOrDefault()!;

            return language;
        }
        private static HttpContext GetHttpContext()
        {
            var httpContextAccessor = new HttpContextAccessor();
            return httpContextAccessor.HttpContext;
        }
    }
}
