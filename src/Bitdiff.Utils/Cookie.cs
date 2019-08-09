using System;
using System.Web;

namespace Bitdiff.Utils
{
    public class Cookie : ICookie
    {
        private readonly IHttpContextFactory _contextFactory;
        public static string DefaultDomain;

        public Cookie(IHttpContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private HttpCookieCollection RequestCookies => _contextFactory.GetContext().Request.Cookies;
        private HttpCookieCollection ResponseCookies => _contextFactory.GetContext().Response.Cookies;

        private HttpCookie GetCookieInItems(string name)
        {
            return _contextFactory.GetContext().Items["__request_cookies_" + name] as HttpCookie;
        }

        private void SetCookieInItems(HttpCookie cookie)
        {
            _contextFactory.GetContext().Items["__request_cookies_" + cookie.Name] = cookie;
        }

        public string Get(string name)
        {
            var cookie = GetCookieInItems(name) ?? RequestCookies[name] ?? ResponseCookies[name];
            return cookie?.Value;
        }

        public void Set(string name, string value)
        {
            var httpCookie = new HttpCookie(name, value);

            if (DefaultDomain.HasValue())
            {
                httpCookie.Domain = DefaultDomain;
            }

            ResponseCookies.Add(httpCookie);
            SetCookieInItems(httpCookie);
        }

        public void Set(string name, string value, DateTime expires)
        {
            var httpCookie = new HttpCookie(name, value) { Expires = expires };

            if (DefaultDomain.HasValue())
            {
                httpCookie.Domain = DefaultDomain;
            }

            ResponseCookies.Add(httpCookie);
            SetCookieInItems(httpCookie);
        }

        public void Delete(string name)
        {
            Set(name, null);
        }
    }
}