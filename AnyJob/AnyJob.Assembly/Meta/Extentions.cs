using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace AnyJob.Assembly.Meta
{
    public static class Extentions
    {
        private static ActionOutputMeta CreateOutput(Type actionType)
        {
            var outputAttribute = actionType.GetCustomAttribute<ActionOutputAttribute>();
            if (outputAttribute != null)
            {
                return new ActionOutputMeta()
                {
                    OutputType = outputAttribute.Type,
                    Description = outputAttribute.Description,
                    IsRequired = outputAttribute.IsRequired,
                    Type = outputAttribute.Type.AssemblyQualifiedName,
                };
            }
            else
            {
                return new ActionOutputMeta()
                {
                    OutputType = typeof(object),
                    Description = string.Empty,
                    IsRequired = false,
                    Type = typeof(object).AssemblyQualifiedName
                };
            }
        }

        private static ActionInputMeta CreateInput(PropertyInfo property)
        {
            var inputAttribute = property.GetCustomAttribute<ActionInputAttribute>();
            return new ActionInputMeta()
            {
                Name = property.Name.ToLower(),
                DefaultValue = inputAttribute.Default,
                Description = inputAttribute.Description,
                IsRequired = inputAttribute.IsRequired,
                Type = property.PropertyType.AssemblyQualifiedName,
                IsSecret = inputAttribute.IsSecret,
                Property = property,
            };
        }

        public static bool HasActionMeta(this Type type)
        {
            return type.IsClass && !type.IsAbstract && typeof(IAction).IsAssignableFrom(type) && Attribute.IsDefined(type, typeof(ActionAttribute));
        }

        public static ActionMeta CreateActionMeta(this Type actionType)
        {
            if (!actionType.HasActionMeta())
            {
                throw new ActionException($"The type \"{actionType.AssemblyQualifiedName}\" is not a action type.");
            }

            var attr = actionType.GetCustomAttribute<ActionAttribute>();
            var outputMeta = CreateOutput(actionType);
            var inputMetas = actionType.GetProperties().Where(t => Attribute.IsDefined(t, typeof(ActionInputAttribute))).Select(CreateInput);
            return new ActionMeta()
            {
                Ref = attr.RefName,
                Description = attr.Description,
                DisplayFormat = attr.DisplayFormat,
                Kind = "assembly",
                Output = outputMeta,
                InputMetas = inputMetas,
                Enabled = true,
                ActionType = actionType,
            };


        }
    }
}
