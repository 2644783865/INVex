using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Attributes.Base
{
    public interface IAttributeStep : IPathElement
    {
        string Name { get; }
        IAttributeModel Attribute { get; set; }
    }
}
