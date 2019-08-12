using INVex.Core.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands
{
    public class ExecutionManager : ExecutionManagerBase
    {
        public override ExecutionResult ProcessCommand(ICommandBase command)
        {
            ExecutionResult result;
            if (this.CommandNotifiers.ContainsKey(command.Name))
            {
                ICommandNotifyHandler notifyHandler = this.CommandNotifiers[command.Name];

                notifyHandler.BeforeExecute(command);
                result = command.Execute();
                notifyHandler.AfterExecute(command);
            }
            else
            {
                result = command.Execute();
            }
            return result;
        }

        public override void RegisterCommandProcessor(ICommandProcessor commandProcessor)
        {
            //this.CommandHandlers.Add()
        }
    }
}
