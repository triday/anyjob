using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AnyJob.Assembly.Meta;
using AnyJob.Meta;
using System.Linq;
namespace AnyJob.Assembly
{
    public class AssemblyActionEntry : IActionEntry
    {
        private ActionMeta meta;
        private Dictionary<string, ActionInputMeta> inputs;
        public AssemblyActionEntry(Type actionType)
        {
            this.meta = actionType.CreateActionMeta();
            this.inputs = meta.InputMetas.ToDictionary(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
        }
        public IActionMeta MetaInfo
        {
            get
            {
                return meta;
            }
        }

        public IAction CreateInstance(IActionParameters parameters)
        {
            var instance = Activator.CreateInstance(this.meta.ActionType);

            foreach (var kv in parameters.Inputs)
            {
                if (this.inputs.ContainsKey(kv.Key))
                {
                    var convertedValue = kv.Value;

                    this.inputs[kv.Key].Property.SetValue(instance, convertedValue);
                }
                else
                {
                    //多余的输入参数...
                }
            }

            return instance as IAction;
        }

    }
}
