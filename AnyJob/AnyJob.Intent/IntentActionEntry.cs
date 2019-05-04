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

        }
        public IActionMeta MetaInfo => throw new NotImplementedException();

        public IAction CreateInstance(IActionParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
