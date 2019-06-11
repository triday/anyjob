using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AnyJob
{
    public class ActionName : IActionName
    {
        const char ACTION_NAME_SPLIT_CHAR = '.';
        static Regex ActionFullname_Regex = new Regex(@"^(?<pack>(\w+\.)*)(?<name>\w+)$", RegexOptions.Compiled);

        public ActionName(string pack, string name)
        {
            this.Pack = pack;
            this.Name = name;
            this.FullName = BuildFullName(pack, name);
        }

        public string Name { get; private set; }

        public string Pack { get; private set; }

        public string FullName { get; private set; }

        public string Version => throw new NotImplementedException();

        private string BuildFullName(string pack, string name)
        {
            List<string> nameParts = new List<string>(2);
            if (string.IsNullOrEmpty(pack))
            {
                nameParts.Add(pack);
            }
            if (string.IsNullOrEmpty(name))
            {
                nameParts.Add(name);
            }
            return string.Join(ACTION_NAME_SPLIT_CHAR, nameParts);
        }

        public override string ToString()
        {
            return this.FullName;
        }
        public static ActionName FromFullName(string fullName)
        {
            var match = ActionFullname_Regex.Match(fullName);
            if (!match.Success)
            {

            }


            var lastSplitIndex = fullName.LastIndexOf(ACTION_NAME_SPLIT_CHAR);
            if (lastSplitIndex < 0)
            {
                var packName = fullName.Substring(0, lastSplitIndex);
                var actionName = fullName.Substring(lastSplitIndex + 1);
                return new ActionName(packName, actionName);
            }
            else
            {
                return new ActionName(string.Empty, fullName);
            }
        }
    }
}
