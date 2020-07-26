using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YS.Knife.Test;

namespace AnyJob.Runner.Python.IntegrationTest
{
    //[TestClass]
    public class TestEnvironment
    {
        [AssemblyInitialize()]
        public static void Setup(TestContext _)
        {
            var availablePort = Utility.GetAvailableTcpPort(80);
            var readyPort = Utility.GetAvailableTcpPort(8901);
            //StartContainer(availablePort);
            //SetConnectionString(availablePort);
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
                ["REPORT_TO_HOST_PORT"] = reportToHostPort
            });
        }
        private static void SetConnectionString(uint port, string password)
        {
            // var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder
            // {
            //     Server = "127.0.0.1",
            //     Port = port,
            //     Database = "SequenceContext",
            //     UserID = "root",
            //     Password = password
            // };
            // Environment.SetEnvironmentVariable("ConnectionStrings__@DbType", "mysql");
            // Environment.SetEnvironmentVariable("ConnectionStrings__SequenceContext", mySqlConnectionStringBuilder.ConnectionString);
        }




    }
}
