using AnyJob.App.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnyJob.Process;

namespace AnyJob.App
{
    public class AppAction : ProcessAction
    {
        public AppAction(AppInfo appInfo)
        {
            this.AppInfo = appInfo;
        }
        public AppInfo AppInfo { get; private set; }

        protected override (string FileName, string Arguments) OnGetCommands(IActionContext context)
        {
            var items = this.AppInfo.Command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var args = context.Parameters.Inputs;
            var translateItems = items.Select(p => Translate(p, args));
            return (translateItems.First(), string.Join(' ', translateItems.Skip(1)));
        }
        private string Translate(string item, IDictionary<string, object> input)
        {
            if (item.StartsWith("${") && item.EndsWith("}"))
            {
                string key = item.Substring(2, item.Length - 3);
                return Convert.ToString(input[key]);
            }
            else
            {
                return item;
            }
        }
    }
}
