using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyJob
{
    public class Job
    {
        /// <summary>
        /// 获取或设置执行Id
        /// </summary>
        public string ExecutionId { get; set; }
        /// <summary>
        /// 获取或设置启动参数信息等
        /// </summary>
        public JobStartInfo StartInfo { get; set; }
        /// <summary>
        /// 获取或设置Spy
        /// </summary>
        public IExecuteSpy Spy { get; set; }
        /// <summary>
        /// 获取或设置Task
        /// </summary>
        public Task<ExecuteResult> Task { get; set; }
    }
}
