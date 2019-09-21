using INVex.ORM.Expressions.Base;
using INVex.ORM.Expressions.Objects;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Logical
{
    public class AttributeCondition : ConditionBase
    {
        private IAttributeModel attributeLeft;
        private IAttributeModel attributeRight;

        private OperatorType operatorType;

        public AttributeCondition(IAttributeModel attributeLeft, IAttributeModel attributeRight, OperatorType operatorType)
        {
            this.attributeLeft = attributeLeft;
            this.attributeRight = attributeRight;
            this.operatorType = operatorType;
        }

        public AttributeCondition(IAttributePath attributePathLeft, IAttributePath attributePathRight, OperatorType operatorType, IObjectInstance ownerInstance)
            : this(PathProcessor.ProcessPath(attributePathLeft, ownerInstance), PathProcessor.ProcessPath(attributePathRight, ownerInstance),
                   operatorType)
        {
            this.Owner = ownerInstance;
        }

        public override bool IsTrue()
        {
            switch (this.operatorType)
            {
                case OperatorType.Equal:
                    return this.attributeLeft.Field.Equals(this.attributeRight.Field);
                case OperatorType.NotEqual:
                    return !this.attributeLeft.Field.Equals(this.attributeRight.Field);
                default:
                    throw new NotImplementedException("Использован неизвестный оператор сравнения");
            }
        }

        public override string ToSql()
        {
            throw new NotImplementedException();
        }
    }
}
