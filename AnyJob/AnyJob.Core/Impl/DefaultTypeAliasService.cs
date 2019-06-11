using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(ITypeAliasService))]
    public class DefaultTypeAliasService : ITypeAliasService
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        static DefaultTypeAliasService()
        {
            dic.Add("none", typeof(DBNull).FullName);
            dic.Add("null", typeof(DBNull).FullName);
            dic.Add("dbnull", typeof(DBNull).FullName);
            dic.Add("string", typeof(string).FullName);
            dic.Add("boolean", typeof(bool).FullName);
            dic.Add("char", typeof(char).FullName);
            dic.Add("sbyte", typeof(sbyte).FullName);
            dic.Add("byte", typeof(byte).FullName);
            dic.Add("short", typeof(short).FullName);
            dic.Add("ushort", typeof(ushort).FullName);
            dic.Add("int", typeof(int).FullName);
            dic.Add("uint", typeof(uint).FullName);
            dic.Add("long",typeof(long).FullName);
            dic.Add("ulong", typeof(ulong).FullName);
            dic.Add("single", typeof(float).FullName);
            dic.Add("float", typeof(float).FullName);
            dic.Add("number", typeof(double).FullName);
            dic.Add("double", typeof(double).FullName);
            dic.Add("decimal", typeof(decimal).FullName);

            dic.Add("object", typeof(object).FullName);
            dic.Add("datetime", typeof(DateTime).FullName);
            dic.Add("timespan", typeof(TimeSpan).FullName);
        }
       
        public string TranslateShortTypeName(string name)
        {
            if (dic.TryGetValue(name, out string fullName))
            {
                return fullName;
            }
            return name;
        }
    }
}
