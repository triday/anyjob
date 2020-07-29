using System.Text;

namespace AnyJob
{
    public class ActionName : IActionName
    {
        const char PROVIDER_NAME_SPLIT_CHAR = ':';
        const char ACTION_NAME_SPLIT_CHAR = '.';
        const char VERSION_SPLIT_CHAR = '@';

        public string Name { get; set; }

        public string Pack { get; set; }

        public string Provider { get; set; }
        public string Version { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Provider))
            {
                sb.Append(this.Provider);
                sb.Append(PROVIDER_NAME_SPLIT_CHAR);
            }
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
