using System.Threading.Tasks;
namespace AnyJob.Runner.Internal.IntegrationTest.Actions
{
    public class Add : IAction
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }

        public object Run(IActionContext context)
        {
            return this.Num1 + this.Num2;
        }
    }
    public class AddTask : IAction
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }

        public object Run(IActionContext context)
        {
            return Task.FromResult(this.Num1 + this.Num2);
        }
    }
}
