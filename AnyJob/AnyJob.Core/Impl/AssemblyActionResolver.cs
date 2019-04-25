using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using AnyJob.Meta;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionResolverService))]
    public class AssemblyActionResolver : IActionResolverService
    {
        static Dictionary<string, Tuple<ActionMeta,Type>> actionMaps = new Dictionary<string, Tuple<ActionMeta, Type>>();
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
                        where p.IsClass && Attribute.IsDefined(p, typeof(ActionAttribute))
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
            if (actionMaps.TryGetValue(actionRef, out var item))
            {
                return new ActionEntry()
                {
                    Action = Activator.CreateInstance(item.Item2) as IAction,
                    Meta = item.Item1,
                };
            }
            else {
                return null;
            }
        }


        
       
    }
}
