using INVex.Core.Fields.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Fields
{
    public class CollectionField : ObjectField
    {
        private List<IBaseField> items = new List<IBaseField>();

        /// <summary>
        /// Flags about that collection can use any field derived from ObjectField
        /// </summary>
        public bool isUniversal { get; private set; } = false;

        /// <summary>
        /// Type of items that can be used in collection if not universal
        /// default - ObjectField
        /// </summary>
        private string itemsType = "ObjectField";

        /// <summary>
        /// Value is readonly collection with items
        /// </summary>
        public new IReadOnlyCollection<IBaseField> Value
        {
            get
            {
                return this.items.AsReadOnly();
            }
        }

        public virtual void Add(IBaseField item)
        {
            string itemType = string.Empty;
            if (this.isUniversal)
            {
                this.items.Add(item);
            }
            else if(this.itemsType == (itemType = item.GetType().Name))
            {
                this.items.Add(item);
            }
            else
            {
                throw new Exception(string.Format("Попытка добавить в коллекцию значение типа {0}. Ожидалось значение типа {1}", itemType, this.itemsType));
            }
        }

        public CollectionField()
        {
            this.isUniversal = true;
        }

        public CollectionField(string itemsType)
        {
            this.itemsType = itemsType;
        }

        public CollectionField(bool universal)
        {
            this.isUniversal = universal;
        }
    }
}
