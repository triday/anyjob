using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnyJob.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Schema;

namespace AnyJob.Impl
{
    [YS.Knife.ServiceClass(Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultActionMetaService : IActionMetaService
    {
        private IOptions<PackOption> packOption;
        private IObjectStoreService fileStoreService;
        public DefaultActionMetaService(IObjectStoreService fileStoreService, IOptions<PackOption> packOption)
        {
            this.fileStoreService = fileStoreService;
            this.packOption = packOption;
        }
        public IActionMeta GetActionMeta(IActionName actionName)
        {
            try
            {
                string metaFile = this.OnGetMetaFile(actionName);
                var metaInfo = fileStoreService.GetObject<MetaInfo>(metaFile);
                return ConvertToActionMeta(metaInfo);
            }
            catch (System.Exception ex)
            {
                throw Errors.GetMetaInfoError(ex, actionName);
            }
        }
        protected virtual string OnGetMetaFile(IActionName actionName)
        {
            _ = actionName ?? throw new ArgumentNullException(nameof(actionName));
            List<string> paths = new List<string>()
            {
                packOption.Value.RootDir
            };
            paths.Add(actionName.Pack);
            if (!string.IsNullOrEmpty(actionName.Version))
            {
                paths.Add(actionName.Version);
            }
            paths.Add($"{actionName.Name}.action");
            return Path.Combine(paths.ToArray());
        }
        protected IActionMeta ConvertToActionMeta(MetaInfo metaInfo)
        {
            if (metaInfo == null) return null;
            var inputList = metaInfo.Inputs ?? new Dictionary<string, JSchema>();
            return new ActionMeta()
            {
                Kind = metaInfo.Kind,
                Description = metaInfo.Description,
                Enabled = metaInfo.Enabled,
                EntryPoint = metaInfo.EntryPoint,
                Tags = metaInfo.Tags,
                Inputs = inputList.ToDictionary(p => p.Key, p => new JsonSchemaType(p.Value) as IActionType),
                Output = new JsonSchemaType(metaInfo.Output)
            };
        }



        protected class MetaInfo
        {
            public string Kind { get; set; }

            public string Description { get; set; }

            public string EntryPoint { get; set; }

            public bool Enabled { get; set; }

            public List<string> Tags { get; set; }

            public Dictionary<string, JSchema> Inputs { get; set; }

            public JSchema Output { get; set; }

        }


    }
}
