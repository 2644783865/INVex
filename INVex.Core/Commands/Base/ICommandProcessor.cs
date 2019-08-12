using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Commands.Base
{
    public interface ICommandProcessor
    {
        void Prepare(ICommandBase command);
        void Execute(ICommandBase command);
        void PostExecute(ICommandBase command);
    }
}
