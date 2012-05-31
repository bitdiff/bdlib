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
        public string Should_truncate(string input, int maxLength)
        {
            return input.Truncate(maxLength);
        }
    }
}
