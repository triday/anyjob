using System;

namespace AnyJob.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SchemaAttribute : Attribute
    {
        readonly string schemaFile;

        // This is a positional argument
        public SchemaAttribute(string schemaFile)
        {
            this.schemaFile = schemaFile;

        }

        public string SchemaFile
        {
            get { return schemaFile; }
        }
    }
}
