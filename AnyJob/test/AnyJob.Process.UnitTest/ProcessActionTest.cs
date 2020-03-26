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

        class PingProcessAction : ProcessAction
        {
            protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
            {
                return ("ping", "127.0.0.1 -n 2",string.Empty);
            }
        }

        class EchoProcessAction : ProcessAction
        {
            protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
            {
                return ("cmd", "/c echo hello",string.Empty);
            }
        }

        class JavaVersionProcessAction : ProcessAction
        {
            protected override (string FileName, string Arguments, string StandardInput) OnGetCommands(IActionContext context)
            {
                return ("java", "-version",string.Empty);
            }
        }
    }
}
