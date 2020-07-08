using System.Collections.Generic;
using System.Linq;
namespace AnyJob
{
    public class ExecutePath : IExecutePath
    {
        public ExecutePath(params string[] paths)
        {
            this.Paths = new List<string>(paths).AsReadOnly();
        }
        public IReadOnlyList<string> Paths { get; private set; }
        public string RootId
        {
            get
            {
                return this.Paths.Count > 0 ? this.Paths[0] : null;
            }
        }
        public string ParentId
        {

            get
            {
                return this.Paths.Count > 1 ? this.Paths[this.Paths.Count - 2] : null;
            }
        }
        public string ExecuteId
        {
            get
            {
                return this.Paths.Count > 0 ? this.Paths[this.Paths.Count - 1] : null;
            }
        }
        public int Depth
        {
            get
            {
                return this.Paths.Count;
            }
        }
        public override string ToString()
        {
            return string.Format("ExecuteID:{0}", ExecuteId);
        }
        public IExecutePath NewSubPath(string subExecuteId)
        {
            var pathList = this.Paths.ToList();
            pathList.Add(subExecuteId);
            return new ExecutePath(pathList.ToArray());
        }

        public static ExecutePath RootPath(string rootId)
        {
            return new ExecutePath(rootId);
        }
    }
}
