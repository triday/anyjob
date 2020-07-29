using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Test;

namespace AnyJob.Runner.App.IntegrationTest
{
    [TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize()]
        public static void Setup(TestContext _)
        {
            var availablePort = Utility.GetAvailableTcpPort(80);
            var readyPort = Utility.GetAvailableTcpPort(8901);
            StartContainer(availablePort, readyPort);
        }

        [AssemblyCleanup()]
        public static void TearDown()
        {
            DockerCompose.Down();
        }


        private static void StartContainer(uint providerPort, uint reportToHostPort)
        {
            DockerCompose.Up(new Dictionary<string, object>
            {
                ["PROVIDER_PORT"] = providerPort,
            }, (int)reportToHostPort, 20);

            Environment.SetEnvironmentVariable("pack__providers__default", $"http://localhost:{providerPort}");
        }

    }
}
