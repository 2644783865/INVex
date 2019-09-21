using INVex.Common.Common;
using INVex.ORM.Common;
using INVex.ORM.DataBase.Common;
using INVex.ORM.DataBase.SQLServer;
using INVex.ORM.Exceptions;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Holders.Base;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;

namespace INVex.ORM.Holders
{
    public class ObjectModelsHolder : SingletonBase<ObjectModelsHolder>
    {
        public IObjectHolder Holder { get; private set; }

        public ObjectModelsHolder()
        {
            
        }

        public void SetupHolder(IObjectHolder objectHolder)
        {
            this.Holder = objectHolder;
        }
    }
}
