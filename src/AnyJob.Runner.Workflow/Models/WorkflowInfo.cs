using System.Collections.Generic;

namespace AnyJob.Runner.Workflow.Models
{
    public class WorkflowInfo
    {
        /// <summary>
        /// 获取或设置工作流文件的版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 获取或设置工作流预处理分组信息
        /// </summary>
        public GroupInfo Setup { get; set; }
        /// <summary>
        /// 获取或设置工作流执行分组信息
        /// </summary>
        public GroupInfo Body { get; set; }
        /// <summary>
        /// 获取或设置工作流后处理分组信息
        /// </summary>
        public GroupInfo TearDown { get; set; }
        /// <summary>
        /// 获取或设置工作流初始的全局变量信息
        /// </summary>
        public Dictionary<string, object> GlobalVars { get; set; }
        /// <summary>
        /// 获取或设置工作流的输出信息
        /// </summary>
        public object Output { get; set; }
    }
}
