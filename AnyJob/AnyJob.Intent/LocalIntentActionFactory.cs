using System;
using System.Collections.Generic;
using System.Text;
using AnyJob;
using AnyJob.Impl;

namespace AnyJob.Intent
{
    [ServiceImplClass(typeof(IActionFactory))]
    public class LocalIntentActionFactory : IActionFactory
    {
        public LocalIntentActionFactory(ISerializeService serializeService)
        {
            this.serializeService = serializeService;
        }
        private ISerializeService serializeService;
        const string BASE_INTENT_FOLDER_NAME = "intents";
        public int Priority => 999;

        public IActionEntry GetEntry(string refName)
        {
            var actionName = Utility.GetActionNameInfoFromRefName(refName);
            List<string> fullPaths = new List<string>()
            {
                 BASE_INTENT_FOLDER_NAME
            };
            fullPaths.AddRange(Utility.GetActionPaths(refName, false));
            fullPaths.Add($"{Utility.GetActionNameFromRefName(refName)}.intent.json");
            var fullName= System.IO.Path.Combine(fullPaths.ToArray());
            if (System.IO.File.Exists(fullName))
            {
                string allText = System.IO.File.ReadAllText(fullName);
                var entityInfo= serializeService.Deserialize<IntentInfo>(allText);
                return new IntentActionEntry(entityInfo);
            }
            return null;
        }
    }
}
