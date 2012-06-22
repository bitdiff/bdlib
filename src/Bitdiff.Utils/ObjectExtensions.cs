using System.IO;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

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

        public static string AsSerializedDataContract<T>(this T input, bool indent = true)
        {
            var serializer = new DataContractSerializer(input.GetType());
            var output = new StringWriter();

            using (var writer = new XmlTextWriter(output) { Formatting = indent ? Formatting.Indented : Formatting.None })
            {
                serializer.WriteObject(writer, input);
            }

            return output.GetStringBuilder().ToString();
        }

        public static string AsSerializedXml<T>(this T input, bool indent = true)
        {
            var serializer = new XmlSerializer(input.GetType());
            var output = new StringWriter();

            using (var writer = XmlWriter.Create(output, new XmlWriterSettings { Indent = indent }))
            {
                serializer.Serialize(writer, input);
                writer.Close();
            }

            return output.GetStringBuilder().ToString();
        }
    }
}