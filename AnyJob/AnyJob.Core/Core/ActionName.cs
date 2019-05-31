using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob
{
    public class ActionName : IActionName
    {
        const char ACTION_NAME_SPLIT_CHAR = '.';
        public ActionName()
        {

        }
        public ActionName(string fullName)
        {
            var lastSplitCharIndex = fullName.LastIndexOf(ACTION_NAME_SPLIT_CHAR);
            if (lastSplitCharIndex >= 0)
            {
                this.Name = fullName.Substring(lastSplitCharIndex + 1);
                this.Pack = fullName.Substring(0, lastSplitCharIndex);
            }
            else
            {
                this.Name = fullName;
            }
        }
        public ActionName(string pack, string name)
        {
            this.Pack = pack;
            this.Name = name;
        }
        public string Name { get; set; }

        public string Pack { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}{1}{2}", Name, ACTION_NAME_SPLIT_CHAR, Pack);
            }
        }
    }
}
