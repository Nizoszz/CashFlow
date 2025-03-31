using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) 
        {
            var supportedLangagues = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureInfo = new CultureInfo("en");
            if (!string.IsNullOrWhiteSpace(requestCulture) && supportedLangagues.Exists(languageInfo => languageInfo.Name.Equals(requestCulture)))
            {
                cultureInfo = new CultureInfo(requestCulture);              
            }
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            await _next(context);
        }
    }
}
