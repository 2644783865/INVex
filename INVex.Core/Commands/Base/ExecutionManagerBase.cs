using INVex.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Base
{
    public class ExecutionManagerBase
    {
        /// <summary>
        /// Notify about command execution process
        /// key - commandName, value - handler
        /// </summary>
        public Dictionary<string, ICommandNotifyHandler> CommandNotifiers { get; protected set; }

        public Dictionary<string, ICommandProcessor> CommandHandlers { get; protected set; }

        public virtual ExecutionResult ProcessCommand(ICommandBase command)
        {
            throw new NotImplementedException();
        }
        public virtual void RegisterCommandProcessor(ICommandProcessor commandProcessor)
        {

        }
    }
}
