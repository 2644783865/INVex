using INVex.Core.Common;
using INVex.Core.Holders.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Holders
{
    /// <summary>
    /// Содержит все системные холдеры.
    /// </summary>
    public class HoldersCollection : SingletonBase<HoldersCollection>
    {
        private Dictionary<string, IHolderBase> holders = new Dictionary<string, IHolderBase>();

        public IHolderBase this[string name]
        {
            get
            {
                if (!holders.ContainsKey(name))
                {
                    throw new Exception(string.Format("Попытка получить незарегистрированный холдер с наименованием {0}", name));
                }
                return holders[name];
            }
        }

        public virtual void RegisterHolder(IHolderBase holder)
        {
            if (string.IsNullOrEmpty(holder.Name))
            {
                throw new Exception("Попытка зарегистрировать холдер без имени.");
            }
            if (this.holders.ContainsKey(holder.Name))
            {
                throw new Exception(string.Format("В коллекции уже зарегистрирован холдер с именем {0}", holder.Name));
            }

            this.holders.Add(holder.Name, holder);
        }


    }
}
