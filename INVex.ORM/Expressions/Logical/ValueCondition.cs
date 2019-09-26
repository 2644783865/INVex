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
    public class ValueCondition : ConditionBase
    {
        private IAttributeModel attributeModel;
        private object value;
        private OperatorType operatorType;

        private IAttributePath attributePath;

        private string attributeName = string.Empty;

        public ValueCondition(IAttributePath attributePath, object value, OperatorType operatorType)
        {
            this.attributePath = attributePath;
            this.value = value;
            this.operatorType = operatorType;
        }

        public ValueCondition(IAttributeModel attributeModel, object value, OperatorType operatorType = OperatorType.Equal)
        {
            this.attributeModel = attributeModel;
            this.value = value;
            this.operatorType = operatorType;
        }

        public ValueCondition(string attributeName, object value, OperatorType operatorType = OperatorType.Equal)
        {
            this.attributeName = attributeName;
            this.value = value;
            this.operatorType = operatorType;
        }

        public override bool IsTrue()
        {
            switch (this.operatorType)
            {
                #warning Уничтожить за такое.
                case OperatorType.Equal:
                    return this.attributeModel.Field.Value.GetHashCode() == value.GetHashCode();
                case OperatorType.NotEqual:
                    return this.attributeModel.Field.Value.GetHashCode() != value.GetHashCode();
                default:
                    throw new NotImplementedException("Использован неизвестный оператор сравнения");
            }
        }

        public override SqlComputedString ToSql()
        {
            if(this.attributeModel == null)
            {
                if(this.attributePath != null)
                {
                    this.attributeModel = PathProcessor.ProcessPath(this.attributePath, this.Owner);
                }
                else
                {
                    this.attributeModel = this.Owner.GetAttribute(attributeName);
                }
            }

            string qr = string.Format("{0} {1} @{2}", attributeModel.Mapping.ColumnName, OperatorProcessor.OperatorToString(this.operatorType), attributeModel.Mapping.AttributeName);
            Dictionary<string, object> sqlParams = new Dictionary<string, object>() { { "@" + attributeModel.Name, this.value } };


            return new SqlComputedString(qr, sqlParams);
        }
    }
}
