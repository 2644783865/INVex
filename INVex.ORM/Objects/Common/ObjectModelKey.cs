using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Common
{
    public sealed class ObjectModelKey : IEquatable<ObjectModelKey>
    {
        private string NameKey;
        private int IdKey;

        public ObjectModelKey()
        {

        }

        public ObjectModelKey(string name)
        {
            this.NameKey = name;
        }

        public ObjectModelKey(int id)
        {
            this.IdKey = id;
        }

        public ObjectModelKey(int id, string name)
        {
            this.NameKey = name;
            this.IdKey = id;
        }

        public ObjectModelKey(IObjectModel model)
        {
            this.NameKey = model.Name;
            this.IdKey = model.Id;
        }

        public bool Equals(ObjectModelKey other)
        {
            return this.NameKey == other.NameKey || this.IdKey == other.IdKey;
        }
    }
}
