using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public static class Utility
    {
        const char ACTION_NAME_SPLIT_CHAR = '.';

        public static string GetActionNamespaceFromRefName(string refName)
        {
            var index = refName.LastIndexOf(ACTION_NAME_SPLIT_CHAR);
            return index < 0 ? refName : refName.Substring(0, index);
        }
        public static string GetActionNameFromRefName(string refName)
        {
            var index = refName.LastIndexOf(ACTION_NAME_SPLIT_CHAR);
            return index < 0 ? refName : refName.Substring(index+1);
        }
        public static (string Namespace, string Name) GetActionNameInfoFromRefName(string refName)
        {
            return (GetActionNamespaceFromRefName(refName), GetActionNameFromRefName(refName));
        }
        public static string[] GetActionPaths(string refName,bool containsName=true)
        {
            if (containsName)
            {
                return refName.Split(ACTION_NAME_SPLIT_CHAR);
            }
            else
            {
                return GetActionNamespaceFromRefName(refName).Split(ACTION_NAME_SPLIT_CHAR);
            }
        }
    }
}
