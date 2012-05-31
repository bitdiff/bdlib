using System.Web;

namespace Bitdiff.Utils
{
    public interface IHttpContextFactory
    {
        HttpContextWrapper GetContext();
    }
}