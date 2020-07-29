namespace AnyJob.NetCore.Demo
{
    public class Hello : IAction
    {
        public string Name { get; set; }

        public object Run(IActionContext context)
        {
            context.Output.WriteLine("Hello,{0}", this.Name);
            return null;
        }
    }
}
