using INVex.Core.Serialize.Base;
using System.Collections.Generic;

namespace INVex.Core.Commands.Base
{
    public interface ICommandBase : IBinarySerializable
    {
        string Name { get; }
        bool SendToServer { get; }
        object InnerObject { get; }
        Dictionary<string, object> Parameters { get; }
        ExecutionResult Execute();
    }
}
