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
            Assert.AreEqual("hello",result);
        }
        class PingProcessAction : ProcessAction
        {
            protected override (string FileName, string Arguments) OnGetCommands(IActionContext context)
            {
                return ("ping", "127.0.0.1 -n 2");
            }
        }
        class EchoProcessAction : ProcessAction
        {
            protected override (string FileName, string Arguments) OnGetCommands(IActionContext context)
            {
                return ("cmd", "/c echo hello");
            }
        }
    }
}
