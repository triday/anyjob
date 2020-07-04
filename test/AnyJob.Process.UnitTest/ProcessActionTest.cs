using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyJob.Process.UnitTest
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
                }
            };
            string result = action.Run(actionContext) as string;
            Assert.AreEqual("hello", result.Trim());
        }

        [TestMethod]
        public void TestJavaVersion()
        {
            IAction action = new JavaVersionProcessAction();
            IActionContext actionContext = new ActionContext()
            {
                RuntimeInfo = new ActionRuntime()
                {
                    WorkingDirectory = System.Environment.CurrentDirectory,
                }
            };
            string result = action.Run(actionContext) as string;
            // Assert.AreEqual("hello", result);
        }

        class PingProcessAction : ProcessAction2
        {
            protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
            {
                return new ProcessExecInput
                {
                    WorkingDir = context.RuntimeInfo.WorkingDirectory,
                    StandardInput = string.Empty,
                    FileName = "ping",
                    Arguments = new[] { "127.0.0.1", "-t", "2" }
                };
            }

        }

        class EchoProcessAction : ProcessAction2
        {
            protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
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

        class JavaVersionProcessAction : ProcessAction2
        {
            protected override ProcessExecInput OnCreateExecInputInfo(IActionContext context)
            {
                return new ProcessExecInput
                {
                    WorkingDir = context.RuntimeInfo.WorkingDirectory,
                    StandardInput = string.Empty,
                    FileName = "java",
                    Arguments = new[] { "-version" }
                };
            }
        }
    }
}
