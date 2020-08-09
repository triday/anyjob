using System;
using System.Collections.Generic;
using System.Text;
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
            return new ActionException();
        }
    }
}
