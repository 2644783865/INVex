using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandProcessorAttribute : Attribute
    {
        public string Name { get; private set; }
        public string ObjectModelName { get; private set; }

        public CommandProcessorAttribute(string name, string objectModelName)
        {
            this.Name = name;
            this.ObjectModelName = objectModelName;
        }

        public CommandProcessorAttribute(string name)
        {
            this.Name = name;
        }
    }
}
