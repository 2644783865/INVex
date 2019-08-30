using INVex.Core.Commands;
using INVex.Core.Commands.Attributes;
using INVex.Core.Commands.Base;
using INVex.Core.Commands.Common;
using INVex.Core.Holders.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Holders.Modify
{
    public class CommandsHolder : IHolderBase
    {
        public const string HolderName = "DEFAULT_COMMANDS_HOLDER";
        public string Name
        {
            get { return CommandsHolder.HolderName; }
        }

        private Dictionary<CommandProcessorKey, ICommandProcessor> registeredCommandProcessors = new Dictionary<CommandProcessorKey, ICommandProcessor>();
        private Dictionary<CommandProcessorKey, ICommandNotifyProcessor> registeredCommandNotify = new Dictionary<CommandProcessorKey, ICommandNotifyProcessor>();

        public virtual void RegisterCommandProcessor(CommandProcessorKey key, ICommandProcessor processor)
        {
            if (registeredCommandProcessors.ContainsKey(key))
            {
                throw new Exception(string.Format("В коллекции обработчиков ({0}) уже зарегистрирован обработчик с именем {1}", this.Name, key.Name));
            }
            this.registeredCommandProcessors.Add(key, processor);
        }

        public ExecutionResult ProcessCommand(ICommandBase command)
        {
            ExecutionResult result = null;

            CommandProcessorKey key = new CommandProcessorKey(command.Name);

            //TODO: Не находит соответствия
            ICommandProcessor commandProcessor = this.registeredCommandProcessors[key];
            if (this.registeredCommandNotify.ContainsKey(key))
            {
                ICommandNotifyProcessor notifyHandler = this.registeredCommandNotify[key];

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

        public virtual void Register()
        {
            HoldersCollection.Current.RegisterHolder(this);
        }
    }
}
