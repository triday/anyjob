using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AnyJob
{
    public class ActionName : IActionName
    {
        const char ACTION_NAME_SPLIT_CHAR = '.';
        const char VERSION_SPLIT_CHAR = '@';

        public string Name { get; set; }

        public string Pack { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Pack))
            {
                sb.Append(this.Pack);
                sb.Append(ACTION_NAME_SPLIT_CHAR);
            }
            sb.Append(this.Name);
            if (!string.IsNullOrEmpty(this.Version))
            {
                sb.Append(VERSION_SPLIT_CHAR);
                sb.Append(this.Version);
            }
            return sb.ToString();
        }

    }
}
