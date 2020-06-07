using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace AnyJob
{
    public static class Extentions
    {
        public static T GetService<T>(this IActionContext context)
        {
            return context.ServiceProvider.GetService<T>();
        }
        public static T GetRequiredService<T>(this IActionContext context)
        {
            return context.ServiceProvider.GetRequiredService<T>();
        }

    }
}
