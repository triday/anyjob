namespace AnyJob
{
    public interface ITraceInfo
    {
        IExecuteContext ExecuteContext { get; set; }
        IActionMeta ActionMeta { get; set; }
        IActionName ActionName { get; set; }
        IActionRuntime ActionRuntime { get; set; }
        IExecuteResult Result { get; set; }
        ExecuteState State { get; set; }
    }
}
