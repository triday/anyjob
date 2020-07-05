using System;
using System.Collections.Generic;
using System.Text;
using AnyJob.DependencyInjection;

namespace AnyJob.Node
{
    [YS.Knife.OptionsClass("node")]
    public class NodeOption
    {
        public string DockerImage { get; set; } = "node:14.5";
        public string NodePath { get; set; } = "node";
        public string WrapperPath { get; set; } = "global/node/node_wrapper.js";
        public string GlobalNodeModulesPath { get; set; } = "global/node/node_modules";
        public string PackNodeModulesPath { get; set; } = "node_modules";

    }
}
