using INVex.Core.Commands.Base;
using INVex.Core.Serialize.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace INVex.Core.Commands
{
    public class Command : ICommandBase, IBinarySerializable
    {
        public string Name { get; protected set; }

        public object InnerObject { get; protected set; }

        public Dictionary<string, object> Parameters { get; protected set; }

        public bool SendToServer { get; set; }


        #region Indexers

        public object this[string name]
        {
            get
            {
                return this.Parameters[name];
            }
        }

        #endregion


        public ExecutionResult Execute()
        {
            return null;
        }


        #region IBinary

        public void Pack(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Unpack(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
