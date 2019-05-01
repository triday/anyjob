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
        public AssemblyActionEntry(Type actionType)
        {
            this.ActionType = actionType;
            var attr = actionType.GetCustomAttribute<ActionAttribute>();
            var outputMeta = CreateOutput(actionType);
            var inputMetas = actionType.GetProperties().Where(t => Attribute.IsDefined(t, typeof(ActionInputAttribute))).Select(CreateInput);
            this.Meta = new ActionMeta()
            {
                Ref = attr.RefName,
                Description = attr.Description,
                DisplayFormat = attr.DisplayFormat,
                Kind = "assembly",
                Output = outputMeta,
                InputMetas = inputMetas
            };
        }
        public IActionMeta Meta { get; private set; }
        public Type ActionType { get; set; }
        private IActionOutputMeta CreateOutput(Type actionType)
        {
            var outputAttribute = actionType.GetCustomAttribute<ActionOutputAttribute>();
            if (outputAttribute != null)
            {
                return new ActionOutputMeta()
                {
                    Description = outputAttribute.Description,
                    IsRequired = outputAttribute.IsRequired,
                    Type = outputAttribute.Type.AssemblyQualifiedName,
                };
            }
            else
            {
                return new ActionOutputMeta()
                {
                    Description = string.Empty,
                    IsRequired = false,
                    Type = typeof(object).AssemblyQualifiedName
                };
            }
        }

        private IActionInputMeta CreateInput(PropertyInfo property)
        {
            var inputAttribute = property.GetCustomAttribute<ActionInputAttribute>();
            return new ActionInputMeta()
            {
                Name = property.Name.ToLower(),
                DefaultValue = inputAttribute.Default,
                Description = inputAttribute.Description,
                IsRequired = inputAttribute.IsRequired,
                Type = property.PropertyType.AssemblyQualifiedName,
                IsSecret = inputAttribute.IsSecret
            };
        }

        public IAction CreateInstance(IActionParameters parameters)
        {
            var instance = Activator.CreateInstance(this.ActionType);

            return instance as IAction;
        }

        #region InnerClass
        public class ActionMeta : IActionMeta
        {
            public string Ref { get; set; }

            public string Description { get; set; }

            public string DisplayFormat { get; set; }

            public string Kind { get; set; }


            public IEnumerable<IActionInputMeta> InputMetas { get; set; }

            public IActionOutputMeta Output { get; set; }
        }
        public class ActionInputMeta : IActionInputMeta
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public object DefaultValue { get; set; }

            public bool IsRequired { get; set; }

            public bool IsSecret { get; set; }

            public string Type { get; set; }
        }
        public class ActionOutputMeta : IActionOutputMeta
        {
            public string Type { get; set; }

            public string Description { get; set; }

            public bool IsRequired { get; set; }
        }

        #endregion
    }
}
