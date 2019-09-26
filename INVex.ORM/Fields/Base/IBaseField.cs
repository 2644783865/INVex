using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Fields.Base
{
    public interface IBaseField 
    {
        object Value { get; }
        bool WasReaded { get; }
        bool WasUpdated { get; }
        void SetValue(object value);
        void ForceSet(object value);
        T GetValue<T>();
    }
}
