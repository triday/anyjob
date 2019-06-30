using AnyJob.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyJob.Node
{
    [ConfigClass("node")]
    public class NodeOption
    {
        public string NodePath { get; set; } = "node";
        public string WrapperPath { get; set; } = "global/node/node_wrapper.js";
        public string GlobalNodeModulesPath { get; set; } = "global/node/node_modules";
        public string PackNodeModulesPath { get; set; } = "node_modules";

    }
}
