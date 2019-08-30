using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Common
{
    public class CommandProcessorKey : IEquatable<CommandProcessorKey>
    {
        public string Name { get; private set; }
        public string ModelName { get; private set; }

        public CommandProcessorKey(string name)
        {
            this.Name = name;
        }

        public CommandProcessorKey(string name, string modelName) : this(name)
        {
            this.ModelName = modelName;
        }

        public bool Equals(CommandProcessorKey other)
        {
            return this.ModelName == other.ModelName && this.Name == other.Name;
        }
    }
}
