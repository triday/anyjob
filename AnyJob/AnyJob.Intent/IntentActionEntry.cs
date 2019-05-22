using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;

namespace AnyJob.Intent
{
    public class IntentActionEntry : IActionEntry
    {
        public IntentActionEntry(IntentInfo intentInfo)
        {
            this.intentInfo = intentInfo;
            this.MetaInfo = new ActionMeta()
            {
                Ref = intentInfo.meta.@ref,
                Description = intentInfo.meta.description,
                DisplayFormat = intentInfo.meta.displayFormat,
                Enabled = intentInfo.meta.enabled,
                Kind = intentInfo.meta.kind,
            };
        }
        private IntentInfo intentInfo;
        public IActionMeta MetaInfo { get; private set; }

        public IAction CreateInstance(ActionParameters parameters)
        {
            return new IntentAction()
            {

                ActionRef = intentInfo.action,
                InputMaps = intentInfo.inputs,
                OutputMap = intentInfo.output,
            };
        }


    }
}
