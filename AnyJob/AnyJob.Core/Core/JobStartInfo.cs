using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AnyJob
{
    /// <summary>
    /// 表示任务的启动信息
    /// </summary>
    public class JobStartInfo
    {
        /// <summary>
        /// 获取或设置执行任务的Action的全名称
        /// </summary>
        public string ActionFullName { get; set; }
        /// <summary>
        /// 或者或设置执行Id
        /// </summary>
        public string ExecutionId { get; set; }
        /// <summary>
        /// 获取或设置执行的参数
        /// </summary>
        public Dictionary<string, object> Inputs { get; set; }
        /// <summary>
        /// 获取或设置执行的上下文信息
        /// </summary>
        public Dictionary<string, object> Context { get; set; }
        /// <summary>
        /// 获取或设置任务的重试次数
        /// </summary>
        public int RetryCount { get; set; }
    }
}
