using AnyJob.Assembly.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyJob.Assembly
{
    public class AssemblyActionFactory : IActionFactory
    {

        private Dictionary<string, IActionEntry> entryMaps = new Dictionary<string, IActionEntry>(StringComparer.CurrentCultureIgnoreCase);

        #region 构造函数
        public AssemblyActionFactory()
        {
            LoadCurrentDomain();
            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) => { LoadAssemblyActions(args.LoadedAssembly); };
        }
        #endregion

        #region Load
        private void LoadCurrentDomain()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssemblyActions(assembly);
            }
        }
        private void LoadAssemblyActions(System.Reflection.Assembly assembly)
        {
            var datas = from p in assembly.GetTypes()
                        where p.IsClass && !p.IsAbstract && typeof(IAction).IsAssignableFrom(p) && Attribute.IsDefined(p, typeof(ActionAttribute))
                        select new AssemblyActionEntry(p);
            foreach (var map in datas)
            {
                entryMaps[map.Meta.Ref] = map;
            }


        }
        #endregion

        public int Priority => 999999;

        public IActionEntry GetEntry(string refName)
        {
            if (entryMaps.TryGetValue(refName, out var entry))
            {
                return entry;
            }
            return null;
        }
    }
}
