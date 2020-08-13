using System.Threading.Tasks;
namespace AnyJob.Internal.Demo
{
    public class Concat : IAction
    {
        public string A { get; set; }
        public string B { get; set; }

        public object Run(IActionContext context)
        {
            return A + B;
        }
    }
    public class ConcatTask : IAction
    {
        public string A { get; set; }
        public string B { get; set; }

        public object Run(IActionContext context)
        {
            return Task.FromResult(A + B);
        }
    }
}
