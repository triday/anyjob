using AnyJob.Assembly.Meta;
using AnyJob.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AnyJob.Assembly
{
    [ServiceImplClass(typeof(IActionDescFactory))]
    public class AssemblyActionDescFactory : IActionDescFactory
    {
        private IServiceProvider serviceProvider;
        public AssemblyActionDescFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public string ActionKind => "assembly";

        public IActionDesc GetActionDesc(IActionEntry entryInfo)
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
