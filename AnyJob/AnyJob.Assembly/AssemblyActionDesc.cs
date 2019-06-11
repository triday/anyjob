using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AnyJob.Assembly.Meta;
using AnyJob.Meta;

namespace AnyJob.Assembly
{
    public class AssemblyActionDesc : ActionMeta, IActionDefination
    {
        public AssemblyActionDesc(Type actionType, IServiceProvider serviceProvider)
        {
            if (!IsActionType(actionType))
            {
                throw ActionException.FromErrorCode(nameof(ErrorCodes.InvalidActionName), actionType.AssemblyQualifiedName);
            }
            var attr = actionType.GetCustomAttribute<ActionAttribute>();


            this.ActionType = actionType;

        }
        public IServiceProvider ServiceProvider { get; set; }
        public Type ActionType { get; set; }

        public List<IActionInputDefination> Inputs => throw new NotImplementedException();

        public IActionOutputDefination Output => throw new NotImplementedException();

        public IAction CreateInstance(ActionParameters parameters)
        {
            var actionInstance = Activator.CreateInstance(this.ActionType) as IAction;

            return actionInstance;
        }

        private bool IsActionType(Type type)
        {
            return type.IsClass && !type.IsAbstract && typeof(IAction).IsAssignableFrom(type) && Attribute.IsDefined(type, typeof(ActionAttribute));
        }
    }
}
