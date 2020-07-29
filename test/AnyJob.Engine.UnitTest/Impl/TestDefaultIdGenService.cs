using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AnyJob.Engine.UnitTest.Impl
{
    [TestClass]
    public class TestDefaultIdGenService : YS.Knife.Hosting.KnifeHost
    {
        [TestMethod]
        public void ShouldUnique()
        {
            var idGenService = this.GetService<IIdGenService>();
            var set = Enumerable.Range(0, 100).Select(_ => idGenService.NewId()).ToHashSet();
            set.Count.Should().Be(100);
        }
    }
}
