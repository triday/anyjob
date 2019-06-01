using AnyJob.Assembly.Meta;
using AnyJob.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AnyJob.Assembly
{
    [ServiceImplClass(typeof(IActionDefinationFactory))]
    public class AssemblyActionDescFactory : IActionDefinationFactory
    {
        private IServiceProvider serviceProvider;
        public AssemblyActionDescFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public string ActionKind => "assembly";

        public IActionDefination GetActionDefination(IActionMeta entryInfo)
        {
            Type actionType = Type.GetType(entryInfo.EntryPoint);
            if (!typeof(IAction).IsAssignableFrom(actionType))
            {
                throw new ActionException(string.Format("Action 类型必须实现接口{0}",nameof(IAction)));
            }
            return new AssemblyActionDesc(actionType, this.serviceProvider);
        }
    }
}
