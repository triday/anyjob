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
}
