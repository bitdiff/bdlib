using Bitdiff.Utils;
using NUnit.Framework;

namespace Bitdiff.Tests.Utils
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void EnsureNotNull_should_not_create_new_when_source_is_not_null()
        {
            var o = new object();
            var r = o.EnsureNotNull();

            Assert.That(o.Equals(r));
        }

        [Test]
        public void EnsureNotNull_should_create_new_when_source_is_null()
        {
            object o = null;
            var r = o.EnsureNotNull();

            Assert.That(r != null);
        }
    }
}