using System.Threading.Tasks;
namespace AnyJob.Runner.Internal.IntegrationTest.Actions
{
    public class Hello : IAction
    {
        public string Name { get; set; }

        public object Run(IActionContext context)
        {
            context.Logger.WriteLine("Hello,{0}", this.Name);
            return null;
        }
    }
    public class HelloTask : IAction
    {
        public string Name { get; set; }

        public object Run(IActionContext context)
        {
            context.Logger.WriteLine("Hello,{0}", this.Name);
            return Task.CompletedTask;
        }
    }
}
