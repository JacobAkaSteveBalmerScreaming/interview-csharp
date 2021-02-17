using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        [OneTimeSetUp]
        public void ClassInit() { }

        [SetUp]
        public void Init() { }

        [TearDown]
        public void Cleanup() { }

        [OneTimeTearDown]
        public void ClassCleanup() { }

        [Test]
        public async Task SomeTest()
        {
            return;
        }
    }


}
