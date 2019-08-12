using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Base
{
    public interface ICommandNotifyHandler
    {
        void BeforeExecute(ICommandBase command);
        void AfterExecute(ICommandBase command);
    }
}
