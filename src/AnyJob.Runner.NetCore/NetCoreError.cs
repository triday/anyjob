using Microsoft.Extensions.Localization;
using YS.Knife;

namespace AnyJob.Runner.NetCore
{
    [ResourceClass]
    public class NetCoreError
    {
        private readonly IStringLocalizer stringLocalizer;

        public NetCoreError(IStringLocalizer<NetCoreError> stringLocalizer)
        {
            this.stringLocalizer = stringLocalizer;
        }


        public ActionException InvalidEntryFormat(string entry)
        {
            return CreateException("E400100", entry);
        }
        public ActionException CanNotFindAssemblyFile(string assemblyFile)
        {
            return CreateException("E400109", assemblyFile);
        }

        private ActionException CreateException(string code, params object[] args)
        {
            string message = stringLocalizer[code, args];
            return new ActionException(code, message);
        }
    }
}
