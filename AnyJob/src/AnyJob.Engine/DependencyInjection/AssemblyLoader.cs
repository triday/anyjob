using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AnyJob.DependencyInjection
{
    public static class AssemblyLoader
    {
        public static void LoadAssemblies(params string[] patterns)
        {
            foreach (var pattern in patterns)
            {
                foreach (var dll in Directory.GetFiles(Environment.CurrentDirectory, pattern))
                {
                    Assembly.LoadFrom(dll);
                }
            }
        }
    }
}
