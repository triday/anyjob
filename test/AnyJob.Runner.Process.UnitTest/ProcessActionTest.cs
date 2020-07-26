using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Runner.Process.UnitTest
{
    [TestClass]
    public class ProcessActionTest
    {
        [TestMethod]
        public void TestPingProcess()
        {
            IAction action = new PingProcessAction();
            IActionContext actionContext = new ActionContext()
            {
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.Environment.CurrentDirectory,
                }
            };
            object result = action.Run(actionContext);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestEchoProcess()
        {
            IAction action = new EchoProcessAction();
            IActionContext actionContext = new ActionContext()
            {
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.Environment.CurrentDirectory,
                },
                Output = new ActionLogger(),
            };
            object result = action.Run(actionContext);
            Assert.AreEqual(0, result);
            Assert.AreEqual("hello", actionContext.Output.ToString().Trim());
        }



        class PingProcessAction : ProcessAction2
        {
            private bool IsWindows = (int)(System.Environment.OSVersion.Platform) < 4;
            protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
            {
                return new ProcessExecInput
                {
                    WorkingDir = context.RuntimeInfo.WorkingDirectory,
                    StandardInput = string.Empty,
                    FileName = "ping",
                    Arguments = IsWindows ? new[] { "127.0.0.1", "-n", "2" } : new[] { "127.0.0.1", "-c", "2" }
                };
            }

        }

        class EchoProcessAction : ProcessAction2
        {
            private bool IsWindows = (int)(System.Environment.OSVersion.Platform) < 4;
            protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
            {
                if (IsWindows)
                {
                    return new ProcessExecInput
                    {
                        WorkingDir = context.RuntimeInfo.WorkingDirectory,
                        StandardInput = string.Empty,
                        FileName = "cmd",
                        Arguments = new[] { "/c", "\"echo hello\"" }
                    };
                }
                else
                {
                    return new ProcessExecInput
                    {
                        WorkingDir = context.RuntimeInfo.WorkingDirectory,
                        StandardInput = string.Empty,
                        FileName = "sh",
                        Arguments = new[] { "-c", "\"echo hello\"" }
                    };
                }

            }
        }


    }
}
