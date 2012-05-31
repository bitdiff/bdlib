using Bitdiff.Utils;
using NUnit.Framework;

namespace Bitdiff.Tests.Utils
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [TestCase("This is a test", 14, Result = "This is a test")]
        [TestCase("This is a test", 12, Result = "This is a...")]
        [TestCase("Thisisatest", 10, Result = "Thisisa...")]
        public string Truncate(string input, int maxLength)
        {
            return input.Truncate(maxLength);
        }

        [Test]
        [TestCase("", Result = null)]
        [TestCase(null, Result = null)]
        public string As_null_if_empty(string input)
        {
            return input.AsNullIfEmpty();
        }

        [Test]
        [TestCase("", Result = "")]
        [TestCase(null, Result = "")]
        public string As_empty_if_null(string input)
        {
            return input.AsEmptyIfNull();
        }

        [Test]
        [TestCase(null, Result = "")]
        public string Safe_trim(string input)
        {
            return input.SafeTrim();
        }

        [Test]
        [TestCase("<div><a href=\"#foo\">link</a></div>", Result = "link")]
        public string Strip_html_tags(string input)
        {
            return input.StripHtmlTags();
        }

        [Test]
        [TestCase("THIS is a Test. SECOND sentenCe.", Result = "This Is A Test. Second Sentence.")]
        public string To_title_case(string input)
        {
            return input.ToTitleCase();
        }
    }
}
