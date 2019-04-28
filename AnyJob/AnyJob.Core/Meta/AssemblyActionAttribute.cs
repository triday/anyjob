using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyJob.Meta
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AssemblyActionAttribute : Attribute
    {

        readonly string refName;

        public AssemblyActionAttribute(string refName)
        {
            this.refName = refName;
        }

        public string RefName
        {
            get { return this.refName; }
        }
        public string Description { get; set; }

        public string DisplayFormat { get; set; }

        public static IActionMeta GetActionMeta(Type type)
        {
            AssemblyActionAttribute actionAttr = Attribute.GetCustomAttribute(type, typeof(AssemblyActionAttribute)) as AssemblyActionAttribute;
            if (actionAttr == null) return null;
            var inputs = from p in type.GetProperties()
                         let input = ActionInputAttribute.GetActionInputMeta(p)
                         where input != null
                         select input;
            return new ActionMeta()
            {
                Ref = actionAttr.RefName,
                Description = actionAttr.Description,
                DisplayFormat = actionAttr.DisplayFormat,
                EntryPoint = type.AssemblyQualifiedName,
                Kind = "assembly"
            };
        }
    }
}
