using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AnyJob.Runner.NetCore.Wrapper
{
    class DefaultValue
    {
        static MethodInfo DefaultValueMethod = typeof(DefaultValue).GetMethod(nameof(Default), BindingFlags.Static | BindingFlags.Public| BindingFlags.NonPublic);
        public static object Get(Type type)
        {
            return DefaultValueMethod.MakeGenericMethod(type).Invoke(null, Array.Empty<object>());
        }

        static T Default<T>()
        {
            return default;
        }

    }
}
