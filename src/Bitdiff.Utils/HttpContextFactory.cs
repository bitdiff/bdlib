using System.Web;

namespace Bitdiff.Utils
{
    public class HttpContextFactory : IHttpContextFactory
    {
        public HttpContextWrapper GetContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }
    }
}