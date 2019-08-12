using INVex.Core.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Objects.Common
{
    public class ObjectModelKey : IEquatable<ObjectModelKey>
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
