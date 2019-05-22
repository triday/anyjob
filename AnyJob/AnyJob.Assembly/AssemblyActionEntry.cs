using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AnyJob.Assembly.Meta;
using AnyJob.Meta;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace AnyJob.Assembly
{
    public class AssemblyActionEntry : IActionEntry
    {
        private ActionMeta meta;
        private Dictionary<string, ActionInputMeta> inputs;
        private IConvertService convertService;
        public AssemblyActionEntry(Type actionType, IServiceProvider serviceProvider)
        {
            this.convertService = serviceProvider.GetRequiredService<IConvertService>();
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

        public IAction CreateInstance(ActionParameters parameters)
        {
            var instance = Activator.CreateInstance(this.meta.ActionType);

            foreach (var kv in parameters.Inputs)
            {
                if (this.inputs.ContainsKey(kv.Key))
                {
                    var property = this.inputs[kv.Key].Property;
                    var convertedValue = convertService.Convert(kv.Value,property.PropertyType);
                    property.SetValue(instance, convertedValue);
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
