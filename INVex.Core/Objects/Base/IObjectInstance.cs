using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Objects.Base
{
    public interface IObjectInstance
    {
        IObjectModel Model { get; }
        IDbTable Table { get; }
        Dictionary<string, IAttributeModel> Attributes { get; }
    }
}
