using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AnyJob.Impl
{
    [TestClass]
    public class TestDefaultActionNameResolveService : YS.Knife.Hosting.KnifeHost
    {
        IPackageProviderService packageProviderService = Moq.Mock.Of<IPackageProviderService>(p => p.GetLatestPackageVersion(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()) == "1.1.1");
        protected override void OnLoadCustomService(Microsoft.Extensions.Hosting.HostBuilderContext builder, Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPackageProviderService>(packageProviderService);
        }

        [DataRow("abc.bcd", "default", "abc", "bcd", "1.1.1")]
        [DataRow("abc.bcd@1.0", "default", "abc", "bcd", "1.0")]
        [DataRow("abc.bcd.cde", "default", "abc.bcd", "cde", "1.1.1")]
        [DataRow("abc.bcd.cde@1.0", "default", "abc.bcd", "cde", "1.0")]
        [DataRow("provider:abc.bcd.cde@1.0", "provider", "abc.bcd", "cde", "1.0")]
        [DataRow("provider:abc.bcd.cde", "provider", "abc.bcd", "cde", "1.1.1")]
        [DataRow("provider:abc.bcd", "provider", "abc", "bcd", "1.1.1")]
        [DataTestMethod]
        public void ShouldParseSuccess(string input, string provider, string pack, string name, string version)
        {
            IActionNameResolveService actionNameResolveService = this.GetRequiredService<IActionNameResolveService>();
            IActionName actionName = actionNameResolveService.ResolverName(input);
            actionName.Provider.Should().Be(provider);
            actionName.Pack.Should().Be(pack);
            actionName.Name.Should().Be(name);
            actionName.Version.Should().Be(version);
        }

        [DataRow(null)]
        [DataRow("")]
        [DataRow("abc")]
        [DataRow("^*:a.b")]
        [DataRow("p:a.b@1a")]
        [DataTestMethod]
        public void ShouldThrowActionException(string input)
        {
            IActionNameResolveService actionNameResolveService = this.GetRequiredService<IActionNameResolveService>();
            Action action = () => { actionNameResolveService.ResolverName(input); };

            var exp = Assert.ThrowsException<ActionException>(() =>
            {
                IActionName actionName = actionNameResolveService.ResolverName(input);
            });

            action.Should().Throw<ActionException>()
                .And.ErrorCode.Should().Be(nameof(ErrorCodes.InvalidActionName));
        }

    }
}
