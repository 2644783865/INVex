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
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string InstanceTypeQualifiedName { get; set; }

        public ObjectModel()
        {

        }

        public ObjectModel(int id, string name, string description, string instanceTypeQualifiedName)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.InstanceTypeQualifiedName = instanceTypeQualifiedName;
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
