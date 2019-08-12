using INVex.Core.Commands.Base;
using INVex.Core.Commands.Attributes;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Handlers
{
    public class AutoCommandProcessor : ICommandProcessor
    {
        public AutoCommandProcessor()
        {
            foreach(CommandProcessorAttribute attributeInfo in this.GetType().GetCustomAttributes())
            {
                // TODO Придумать абстракцию какуюнить, а то завязывать всё на одном классе хреново.
            }
        }

        public void Prepare(ICommandBase command)
        {
            throw new NotImplementedException();
        }

        public void Execute(ICommandBase command)
        {
            throw new NotImplementedException();
        }

        public void PostExecute(ICommandBase command)
        {
            throw new NotImplementedException();
        }
    }
}
