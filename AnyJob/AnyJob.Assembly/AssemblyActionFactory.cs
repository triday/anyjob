﻿using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using AnyJob.Impl;
namespace AnyJob.Intent
{
    [ServiceImplClass(typeof(IActionFactory))]
    public class AssemblyActionFactory : IActionFactory
    {
        public string ActionType => ConstCode.ASSEMBLY_ACTION_TYPE;

        public IAction CreateAction(IActionMeta meta, IActionParameters parameters)
        {
            var type = Type.GetType(meta.EntryPoint);
            var instance = Activator.CreateInstance(type);

            return instance as IAction;
        }
    }
}
