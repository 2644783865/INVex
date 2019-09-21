using System;
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
        public bool WasUpdated { get; protected set; } = false;

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
            this.WasUpdated = true;
        }

        public virtual void ForceSet(object value)
        {
            this.Value = value;
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

        public static bool operator ==(ObjectField a, ObjectField b)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(ObjectField a, ObjectField b)
        {
            throw new NotImplementedException();
        }



        public virtual void Pack(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public virtual void Unpack(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is ObjectField field &&
                   EqualityComparer<object>.Default.Equals(this._value, field._value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this._value);
        }

        public virtual bool Equals(IBaseField other)
        {
            throw new NotImplementedException();
        }
    }
}
