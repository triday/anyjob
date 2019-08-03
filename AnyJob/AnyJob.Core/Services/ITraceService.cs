namespace AnyJob
{
    /// <summary>
    /// 表示状态跟踪服务
    /// </summary>
    public interface ITraceService
    {
        /// <summary>
        /// 跟踪执行状态
        /// </summary>
        /// <param name="context">执行上下文</param>
        /// <param name="state">执行状态</param>
        /// <param name="result">执行结果</param>
        void TraceState(IExecuteContext context, ExecuteState state, ExecuteResult result);
    }
}
