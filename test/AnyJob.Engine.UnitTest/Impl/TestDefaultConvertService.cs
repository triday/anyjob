using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AnyJob.Impl
{
    [TestClass]
    public class TestDefaultConvertService : YS.Knife.Hosting.KnifeHost
    {
        [DataRow(0.0, typeof(bool), false)]
        [DataRow(0, typeof(bool), false)]
        [DataRow(1, typeof(bool), true)]
        [DataRow(1, typeof(long), 1L)]
        [DataRow(1, typeof(double), 1.0)]
        [DataRow("false", typeof(bool), false)]
        [DataRow("123", typeof(long), 123L)]
        [DataRow("123.0", typeof(double), 123.0)]
        [DataTestMethod]
        public void ShouldConvertToTargetType(object source, System.Type targetType, object expected)
        {
            var convertService = this.GetService<IConvertService>();
            var value = convertService.Convert(source, targetType);
            value.Should().Be(expected);
        }

        [DataRow("abc", typeof(double))]
        [DataTestMethod]
        public void ShouldThrowException(object source, System.Type targetType)
        {
            var convertService = this.GetService<IConvertService>();
            Action action = () => convertService.Convert(source, targetType);
            action.Should().Throw<ActionException>().And.ErrorCode.Should().Be(nameof(ErrorCodes.E00004));
        }
    }
}
