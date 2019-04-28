using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    /// <summary>
    /// 表示Action的元数据信息
    /// </summary>
    public class ActionMeta:IActionMeta
    {
        public string Ref { get; set; }

        public string Description { get; set; }

        public string DisplayFormat { get; set; }

        //public List<ActionInputMeta> InputsMeta { get; set; }

        //public ActionOutputMeta OutputMeta { get; set; }

        public string Kind { get; set; }

        public string EntryPoint { get; set; }
    }
}
