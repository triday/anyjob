using System.Collections.Generic;

namespace AnyJob
{
    public interface IExecutePath
    {
        int Depth { get; }
        string ExecuteId { get; }
        string ParentId { get; }
        IReadOnlyList<string> Paths { get; }
        string RootId { get; }

        IExecutePath NewSubPath(string subExecuteId);
    }
}