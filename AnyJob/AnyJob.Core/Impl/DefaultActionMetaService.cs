using AnyJob.Config;
using AnyJob.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyJob.Impl
{
    public class DefaultActionMetaService : IActionMetaService
    {
        private PackOption packOption;
        private IFileStoreService fileStoreService;
        public DefaultActionMetaService(IFileStoreService fileStoreService, PackOption packOption)
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
                packOption.RootDir
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
            var inputList= metaInfo.InputDefines ?? new List<InputInfo>();
            var output = metaInfo.OutputDefine ?? new OutputInfo();
            
            return new ActionMeta()
            {
                ActionKind = metaInfo.ActionKind,
                Description = metaInfo.Description,
                Enabled = metaInfo.Enabled,
                EntryPoint = metaInfo.EntryPoint,
                Tags = metaInfo.Tags,

            };
        }
        protected IActionInputDefination ConvertToActionInput(InputInfo input)
        {
            return null;
        }

        protected IActionOutputDefination ConvertToActionOutput(OutputInfo output)
        {
            return null;
        }
        public class MetaInfo
        {
            public string ActionKind { get; set; }

            public string Description { get; set; }

            public string EntryPoint { get; set; }

            public bool Enabled { get; set; }

            public List<string> Tags { get; set; }

            public List<InputInfo> InputDefines { get; set; }

            public OutputInfo OutputDefine { get; set; }

        }

        public class InputInfo
        {
            public string Name { get; set; }

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
        }

    }
}
