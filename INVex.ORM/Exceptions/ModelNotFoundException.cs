using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.ORM.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public string ModelName;

        public ModelNotFoundException(string modelName)
        {
            this.ModelName = modelName;
        }

        public override string ToString()
        {
            return string.Format("Model with name '{0}' not found", this.ModelName);
        }

    }
}
