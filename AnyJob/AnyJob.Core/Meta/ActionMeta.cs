using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Meta
{
    /// <summary>
    /// 表示Action的元数据信息
    /// </summary>
    public class ActionMeta
    {
        public string Ref { get; set; }

        //public ActionKind Kind { get; set; }

        public List<ActionInputMeta> InputsMeta { get; set; }

        public ActionOutputMeta OutputMeta { get; set; }

        public string DisplayFormat { get; set; }

        public string Description { get; set; }

    }
}
