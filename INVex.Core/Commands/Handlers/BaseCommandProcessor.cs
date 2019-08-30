using INVex.Core.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Handlers
{
    public class BaseCommandProcessor : ICommandProcessor
    {
        public virtual void Prepare(ICommandBase command)
        {
            
        }
        public virtual void Execute(ICommandBase command)
        {
            
        }

        public virtual void PostExecute(ICommandBase command)
        {
            
        }

    }
}
