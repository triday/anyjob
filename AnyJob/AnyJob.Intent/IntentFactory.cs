using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.Meta;
using AnyJob.Impl;
namespace AnyJob.Intent
{
    [ServiceImplClass(typeof(IActionFactory))]
    public class IntentFactory : IActionFactory
    {
        public string ActionKind => ConstCode.INTENT_ACTION_TYPE;

        public IAction CreateAction(IActionMeta meta, IActionParameters parameters)
        {
            IntentAction action = new IntentAction();

            return action;
        }
    }
}
