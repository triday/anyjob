using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using AnyJob.Meta;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionResolverService))]
    public class AssemblyActionResolver : IActionResolverService
    {
        static Dictionary<string, Tuple<ActionMeta,Type>> actionMaps = new Dictionary<string, Tuple<ActionMeta, Type>>();

        public int Priority => 10000;

        static AssemblyActionResolver() {
            LoadCurrentDomain();
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => { LoadAssemblyActions(args.LoadedAssembly); };
        }

        static void LoadCurrentDomain()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssemblyActions(assembly);
            }
        }
        static void LoadAssemblyActions(Assembly assembly)
        {
            var datas = from p in assembly.GetTypes()
                        where p.IsClass &&!p.IsAbstract && typeof(IAction).IsAssignableFrom(p) && Attribute.IsDefined(p, typeof(ActionAttribute))
                        let attr = ActionAttribute.GetActionMeta(p)
                        select new {
                            Name = attr.Ref,
                            Item = Tuple.Create(attr, p)
                        };
            foreach (var map in datas)
            {
                actionMaps[map.Name] = map.Item;
            }
        }

        public ActionEntry ResolveAction(string actionRef)
        {
            if (actionMaps.TryGetValue(actionRef, out var actionInfo))
            {
                return new ActionEntry()
                {
                    Action = Activator.CreateInstance(actionInfo.Item2) as IAction,
                    Meta = actionInfo.Item1,
                };
            }
            else {
                return null;
            }
        }

       
    }
}
