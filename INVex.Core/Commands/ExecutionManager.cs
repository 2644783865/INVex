using INVex.Core.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands
{
    public class ExecutionManager : ExecutionManagerBase
    {
        private static ExecutionManager instance;

        private ExecutionManager()
        { }

        public static ExecutionManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExecutionManager();
                return instance;
            }
        }

        public override ExecutionResult ProcessCommand(ICommandBase command)
        {
            ExecutionResult result = null;
            ICommandProcessor commandProcessor = this.CommandHandlers[command.Name];
            if (this.CommandNotifiers.ContainsKey(command.Name))
            {
                ICommandNotifyProcessor notifyHandler = this.CommandNotifiers[command.Name];

                notifyHandler.BeforeExecute(command);
                commandProcessor.Execute(command);
                //result = commandProcessor.Execute(command);
                notifyHandler.AfterExecute(command);
            }
            else
            {
                //result = commandProcessor.Execute(command);
                commandProcessor.Execute(command);
            }
            return result;
        }
    }
}
