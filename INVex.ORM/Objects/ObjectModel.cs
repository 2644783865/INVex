using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using INVex.Common.Serialize.Base;
using INVex.ORM.Objects.Base;

namespace INVex.ORM.Objects
{
    public class ObjectModel : IObjectModel, IBinarySerializable
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ObjectModel()
        {

        }

        public ObjectModel(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public virtual void Pack(BinaryWriter writer)
        {
            writer.Write(this.GetType().Name);

            writer.Write(this.Id);
            writer.Write(this.Name);
            writer.Write(this.Description);
        }

        public virtual void Unpack(BinaryReader reader)
        {
            this.Id = reader.ReadInt32();
            this.Name = reader.ReadString();
            this.Description = reader.ReadString();
        }
    }
}
