using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Networking.Base
{
    public interface IClientInfo
    {
        string Name { get; }
        string DomainName { get; }
    }
}
