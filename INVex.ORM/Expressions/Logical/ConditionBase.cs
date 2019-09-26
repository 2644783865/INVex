using INVex.ORM.Expressions.Base;
using INVex.ORM.Expressions.Modify;
using INVex.ORM.Expressions.Objects;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Logical
{
    public abstract class ConditionBase : ICondition
    {
        public IObjectInstance Owner { get; protected set; }

        public ConditionBase()
        {

        }

        public abstract bool IsTrue();

        public virtual SqlComputedString ToSql()
        {
            throw new NotImplementedException();
        }

        public void SetOwner(IObjectInstance owner)
        {
            this.Owner = owner;
        }
    }
}
