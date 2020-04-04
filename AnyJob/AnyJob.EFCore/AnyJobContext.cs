using Microsoft.EntityFrameworkCore;
using System;

namespace AnyJob.EFCore
{
    public class AnyJobContext:DbContext
    {
        public virtual DbSet<ExecInfo> ExecInfos { get; set; }
    }
    public class ExecInfo
    {
        public string TraceId { get; set; }

    }
}
