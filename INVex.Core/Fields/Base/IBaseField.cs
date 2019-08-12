using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Fields.Base
{
    public interface IBaseField
    {
        object Value { get; }
        bool WasReaded { get; }
        void SetValue(object value);
        T GetValue<T>();
    }
}
