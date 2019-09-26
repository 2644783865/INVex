using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Expressions.Base
{
    public enum OperatorType
    {
        Equal,
        NotEqual
    }

    public static class OperatorProcessor
    {
        public static string OperatorToString(OperatorType operatorType)
        {
            switch (operatorType)
            {
                case OperatorType.Equal:
                    return "=";
                case OperatorType.NotEqual:
                    return "<>";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
