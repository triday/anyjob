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

    }
}
