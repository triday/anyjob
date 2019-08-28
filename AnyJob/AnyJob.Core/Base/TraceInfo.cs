namespace AnyJob
{
    public class TraceInfo : ITraceInfo
    {
        public IExecuteContext ExecuteContext { get; set; }
        public IActionMeta ActionMeta { get; set; }
        public IActionName ActionName { get; set; }
        public IActionRuntime ActionRuntime { get; set; }
        public IExecuteResult Result { get; set; }
        public ExecuteState State { get; set; }
    }
}
