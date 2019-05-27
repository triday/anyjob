using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Workflow
{
    public class WorkflowInfo
    {
        public GroupInfo Setup { get; set; }

        public GroupInfo Body { get; set; }

        public GroupInfo TearDown { get; set; }
    }
    public class GroupInfo
    {
        public string[] Entry { get; set; }
        public Dictionary<string , TaskInfo> Tasks { get; set; }
    }
    public class TaskInfo
    {
        public string RefName { get; set; }
        public Dictionary<string,object> Inputs { get; set; }

        public TaskChainGroup OnSuccess { get; set; }
        public TaskChainGroup OnError { get; set; }
        public TaskChainGroup OnComplete { get; set; }
    }

    public class TaskChainGroup
    {
        public SwitchKind SwitchKind { get; set; }

        public List<TaskChainInfo> Actions { get; set; }

       
    }

    public class TaskChainInfo
    {
        public string Switch { get; set; }

        public string Task { get; set; }
    }
    public enum SwitchKind
    {
        /// <summary>
        /// 多条分支
        /// </summary>
        Mutile = 0,
        /// <summary>
        /// 单条分支
        /// </summary>
        Single = 1,

    }
}
