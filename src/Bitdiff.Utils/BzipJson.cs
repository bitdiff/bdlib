using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using ICSharpCode.SharpZipLib.BZip2;

namespace Bitdiff.Utils.Utils
{
    public class BzipJson
    {
        private readonly string _json;

        public BzipJson(Stream input)
        {
            _json = ConvertBzipToJson(input);
        }

        public BzipJson(byte[] input)
        {
            _json = ConvertBzipToJson(input);
        }

        public BzipJson(object o)
        {
            _json = JsonSerialize(o);
        }

        public T Get<T>()
        {
            return JsonDeserialize<T>(_json);
        }

        public static T ConvertBzipJsonTo<T>(byte[] input)
        {
            return ConvertBzipJsonTo<T>(new MemoryStream(input));
        }

        public static T ConvertBzipJsonTo<T>(Stream input)
        {
            return JsonDeserialize<T>(ConvertBzipToJson(input));
        }

        public static string ConvertBzipToJson(byte[] input)
        {
            return ConvertBzipToJson(new MemoryStream(input));
        }

        public static string ConvertBzipToJson(Stream input)
        {
            using (var inStream = input)
            using (var outStream = new MemoryStream())
            {
                BZip2.Decompress(inStream, outStream, true);
                return Encoding.UTF8.GetString(outStream.ToArray());
            }
        }

        public static byte[] ConvertObjectToBzipJson(object o)
        {
            string json = JsonSerialize(o);
            byte[] input = Encoding.UTF8.GetBytes(json);

            using (var inStream = new MemoryStream(input))
            using (var outStream = new MemoryStream())
            {
                BZip2.Compress(inStream, outStream, true, 3);
                return outStream.ToArray();
            }
        }

        private static string JsonSerialize(object o)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(o);
        }

        private static T JsonDeserialize<T>(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
    }
}