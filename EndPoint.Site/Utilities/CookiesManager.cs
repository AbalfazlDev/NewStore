using Azure;
using Microsoft.AspNetCore.Http;

namespace EndPoint.Site.Utilities
{
    public class CookiesManager
    {
        public void Add(HttpContext context, string key, string value)
        {
            context.Response.Cookies.Append(key, value, getCookieOptions(context));
        }

        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }

        public string GetValue(HttpContext context, string token)
        {
            string cookieValue;
            if (context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return cookieValue;
            }
            return null;
        }

        public Guid GetBrowserId(HttpContext context)
        {
            string browserId = GetValue(context, "BrowserId");
            if (browserId == null)
            {
                Guid guid = Guid.NewGuid();
                Add(context, "BrowserId", guid.ToString());
                return guid;
            }
            Guid browserGuid;
            Guid.TryParse(browserId,out browserGuid);
            return browserGuid;
        }

        public void Remove( HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
                context.Response.Cookies.Delete(token);
        }

        public CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.HasValue.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(10)
            };
        }
    }
}
