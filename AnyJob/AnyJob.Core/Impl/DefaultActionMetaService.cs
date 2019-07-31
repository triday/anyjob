using AnyJob.Config;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace AnyJob.Impl
{
    [ServiceImplClass(typeof(IActionMetaService))]
    public class DefaultActionMetaService : IActionMetaService
    {
        private IOptions<PackOption> packOption;
        private IFileStoreService fileStoreService;
        public DefaultActionMetaService(IFileStoreService fileStoreService, IOptions<PackOption> packOption)
        {
            this.fileStoreService = fileStoreService;
            this.packOption = packOption;
        }
        public IActionMeta GetActionMeta(IActionName actionName)
        {
            string metaFile = this.OnGetMetaFile(actionName);
            var metaInfo = fileStoreService.ReadObject<MetaInfo>(metaFile);
            return ConvertToActionMeta(metaInfo);
        }
        protected virtual string OnGetMetaFile(IActionName actionName)
        {
            List<string> paths = new List<string>()
            {
                packOption.Value.RootDir
            };
            paths.Add(actionName.Pack);
            if (!string.IsNullOrEmpty(actionName.Version))
            {
                paths.Add(actionName.Version);
            }
            paths.Add($"{actionName.Name}.meta");
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
       

        
        public class MetaInfo
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
