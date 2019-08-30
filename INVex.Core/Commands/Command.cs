using INVex.Core.Commands.Base;
using INVex.Core.Common;
using INVex.Core.Fields;
using INVex.Core.Holders;
using INVex.Core.Holders.Modify;
using INVex.Core.Serialize;
using INVex.Core.Serialize.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace INVex.Core.Commands
{
    public class Command : ICommandBase, IBinarySerializable
    {
        public string Name { get; protected set; }

        public ObjectField InnerObject { get; protected set; }

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

        public Command()
        {

        }

        public Command(string name)
        {
            this.Name = name;

            this.Parameters = new Dictionary<string, object>();
        }

        public virtual ExecutionResult Execute()
        {
            return ((CommandsHolder)HoldersCollection.Current[CommandsHolder.HolderName]).ProcessCommand(this);
        }


        #region IBinary

        public void Pack(BinaryWriter writer)
        {
            writer.Write(this.GetType().Name);

            writer.Write(this.Name);
            SimpleSerializer.PackObject(writer, this.InnerObject);

            SerializationTools.SerializeDict(this.Parameters, writer.BaseStream);

            //byte[] parametersData = SerializationTools.SerializeToBytes(this.Parameters);

            //writer.Write(parametersData.Length);
            //writer.Write(parametersData);

        }

        public void Unpack(BinaryReader reader)
        {
            this.Name = reader.ReadString();

            this.InnerObject = (ObjectField)SimpleSerializer.UnpackObject(reader);

            //int parametersLength = reader.ReadInt32();

            this.Parameters = SerializationTools.DeserializeDict(reader.BaseStream);
            //this.Parameters = (Dictionary<string, object>)SerializationTools.DeserializeFromBytes(test);
        }
        #endregion


    }
}
