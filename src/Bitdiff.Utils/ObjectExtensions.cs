using System.Web.Script.Serialization;

namespace Bitdiff.Utils
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(input);
        }

        public static T EnsureNotNull<T>(this T input) where T : class, new()
        {
            return input ?? new T();
        }
    }
}