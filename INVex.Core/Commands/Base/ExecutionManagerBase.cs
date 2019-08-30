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
        public Dictionary<string, ICommandNotifyProcessor> CommandNotifiers { get; protected set; } = new Dictionary<string, ICommandNotifyProcessor>();

        public Dictionary<string, ICommandProcessor> CommandHandlers { get; protected set; } =  new Dictionary<string, ICommandProcessor>();

        public virtual ExecutionResult ProcessCommand(ICommandBase command)
        {
            throw new NotImplementedException();
        }
    }
}
