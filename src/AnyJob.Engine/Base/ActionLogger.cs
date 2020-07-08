using System;
using System.Text;

namespace AnyJob
{
    public class ActionLogger : IActionLogger
    {
        StringBuilder stringBuilder = new StringBuilder();

        public void WriteLine(string fmt, params object[] args)
        {
            if (String.IsNullOrEmpty(fmt))
            {
                stringBuilder.AppendLine();
            }
            else
            {
                if (args == null || args.Length == 0)
                {
                    stringBuilder.AppendLine(fmt);
                }
                else
                {
                    string line = string.Format(fmt, args);
                    stringBuilder.AppendLine(line);
                }
            }
        }
        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }
}
