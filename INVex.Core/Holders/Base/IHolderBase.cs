using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Holders.Base
{
    public interface IHolderBase
    {
        string Name { get; }
        void Register();
    }
}
