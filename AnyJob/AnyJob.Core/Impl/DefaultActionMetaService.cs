using AnyJob.Config;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AnyJob.DependencyInjection;
using Microsoft.Extensions.Options;

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
            var inputList = metaInfo.Inputs ?? new Dictionary<string, InputInfo>();
            var output = metaInfo.Output ?? new OutputInfo();

            return new ActionMeta()
            {
                Kind = metaInfo.Kind,
                Description = metaInfo.Description,
                Enabled = metaInfo.Enabled,
                EntryPoint = metaInfo.EntryPoint,
                Tags = metaInfo.Tags,
                Inputs = inputList.Select(p => new { Name = p.Key, Input = ConvertToActionInput(p.Value) }).ToDictionary(p => p.Name, p => p.Input),
                Output = ConvertToActionOutput(output)
            };
        }
        protected IActionInputDefination ConvertToActionInput(InputInfo input)
        {
            return new ActionInputDefination()
            {
                Default = input.DefaultValue,
                IsRequired = input.IsRequired,
                IsSecret = input.IsSecret,
                Description = input.Description,

            };
        }

        protected IActionOutputDefination ConvertToActionOutput(OutputInfo output)
        {
            return new ActionOutputDefination()
            {
                Description = output.Description,
                IsRequired = output.IsRequired,
                IsSecret = output.IsSecret
            };
        }
        public class MetaInfo
        {
            public string Kind { get; set; }

            public string Description { get; set; }

            public string EntryPoint { get; set; }

            public bool Enabled { get; set; }

            public List<string> Tags { get; set; }

            public Dictionary<string, InputInfo> Inputs { get; set; }

            public OutputInfo Output { get; set; }

        }

        public class InputInfo
        {

            public string Description { get; set; }

            public object DefaultValue { get; set; }

            public bool IsRequired { get; set; }

            public bool IsSecret { get; set; }

            public string Type { get; set; }
        }

        public class OutputInfo
        {
            public string Type { get; set; }

            public string Description { get; set; }

            public bool IsSecret { get; set; }

            public bool IsRequired { get; set; }
        }

    }
}
