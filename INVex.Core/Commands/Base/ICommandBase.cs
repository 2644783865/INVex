using INVex.Common.Serialize.Base;
using INVex.ORM.Fields;
using System.Collections.Generic;

namespace INVex.Core.Commands.Base
{
    public interface ICommandBase : IBinarySerializable
    {
        string Name { get; }
        bool SendToServer { get; }
        ObjectField InnerObject { get; }
        Dictionary<string, object> Parameters { get; }
        ExecutionResult Execute();
    }
}
