﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using INVex.Common.Serialize.Base;
using INVex.Common.Common;
using INVex.ORM.Fields.Base;

namespace INVex.ORM.Fields
{
    public class ObjectField : IBaseField, IBinarySerializable
    {
        private object _value = null;
        public object Value
        {
            get
            {
                return this._value;
            }
            protected set
            {
                this._value = value;
                this.WasReaded = true;
            }
        }
        public bool WasReaded { get; protected set; } = false;

        public ObjectField()
        {

        }

        public ObjectField(object value)
        {
            this.Value = value;
        }

        public virtual void SetValue(object value)
        {
            this.Value = value;
            this.WasReaded = true;
        }

        public virtual T GetValue<T>()
        {
            try
            {
                return (T)this.Value;
            }
            catch
            {
                throw new Exception("Failed to cast type");
            }
        }

        public virtual void Pack(BinaryWriter writer)
        {
            writer.Write(this.GetType().Name);

            byte[] valueBytes = SerializationTools.SerializeToBytes(this._value);

            writer.Write(valueBytes.Length);
            writer.Write(valueBytes);
        }

        public virtual void Unpack(BinaryReader reader)
        {
            int length = reader.ReadInt32();

            this._value = SerializationTools.DeserializeFromBytes(reader.ReadBytes(length));
        }
    }
}
