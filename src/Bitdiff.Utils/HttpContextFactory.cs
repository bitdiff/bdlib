using System.Web;

namespace Bitdiff.Utils
{
    public class HttpContextFactory : IHttpContextFactory
    {
        public HttpContextWrapper GetContext()
        {
            var context = HttpContext.Current;
            return context != null ? new HttpContextWrapper(context) : null;
        }
    }
}