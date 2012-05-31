using System;
using System.Web;

namespace Bitdiff.Utils
{
    public class Cookie : ICookie
    {
        private readonly IHttpContextFactory _contextFactory;

        public Cookie(IHttpContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private HttpCookieCollection RequestCookies
        {
            get
            {
                return _contextFactory.GetContext().Request.Cookies;
            }
        }

        private HttpCookieCollection ResponseCookies
        {
            get
            {
                return _contextFactory.GetContext().Response.Cookies;
            }
        }

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
            HttpCookie cookie = GetCookieInItems(name) ?? RequestCookies[name] ?? ResponseCookies[name];
            return cookie != null ? cookie.Value : null;
        }

        public void Set(string name, string value)
        {
            var httpCookie = new HttpCookie(name, value);
            ResponseCookies.Add(httpCookie);
            SetCookieInItems(httpCookie);
        }

        public void Set(string name, string value, DateTime expires)
        {
            var httpCookie = new HttpCookie(name, value) { Expires = expires };
            ResponseCookies.Add(httpCookie);
            SetCookieInItems(httpCookie);
        }

        public void Delete(string name)
        {
            Set(name, null);
        }
    }
}