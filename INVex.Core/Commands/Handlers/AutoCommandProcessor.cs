using INVex.Core.Commands.Base;
using INVex.Core.Commands.Attributes;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using INVex.Core.Holders.Base;
using INVex.Core.Holders;
using INVex.Core.Holders.Modify;
using INVex.Core.Commands.Common;
using INVex.Core.Common;

namespace INVex.Core.Commands.Handlers
{
    public class AutoCommandProcessor : BaseCommandProcessor
    {
        public AutoCommandProcessor()
        {
            foreach(CommandProcessorAttribute attributeInfo in this.GetType().GetCustomAttributes())
            {
                CommandProcessorKey key = new CommandProcessorKey(attributeInfo.Name, attributeInfo.ObjectModelName);

                ((CommandsHolder)HoldersCollection.Current[CommandsHolder.HolderName]).RegisterCommandProcessor(key, this);
            }
        }

        public static void RegisterProcessors()
        {
            foreach (Type t in AssemblyUtils.GetTypes(typeof(AutoCommandProcessor)))
            {
                Activator.CreateInstance(t);
            }
        }
    }
}
