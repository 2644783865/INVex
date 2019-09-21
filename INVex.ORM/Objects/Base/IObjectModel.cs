using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Objects.Base
{
    public interface IObjectModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        /// <summary>
        /// Type of instance
        /// </summary>
        string InstanceTypeQualifiedName { get; set; }
    }
}
