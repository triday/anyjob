using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Assembly.Meta
{
    public class AssemblyActionType : IActionType
    {
        private Type type;
        public AssemblyActionType(Type type)
        {
            this.type = type;
        }
        public Type GetRunTimeType()
        {
            return this.type;
        }
    }
}
