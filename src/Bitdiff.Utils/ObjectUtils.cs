using System.Web.Script.Serialization;

namespace Bitdiff.Utils.Utils
{
    public static class ObjectUtils
    {
        public static string ToJson(this object input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(input);
        }
    }
}