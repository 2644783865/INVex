using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System.Collections.Generic;

namespace INVex.ORM.Expressions.Objects
{
    public class PathProcessor
    {
        public static IAttributeModel ProcessPath(IAttributePath path, IObjectInstance ownerInstance)
        {
            List<IAttributeStep> fullStepsStack = path.Steps;
            return PathProcessor.ProcessPath(fullStepsStack, ownerInstance);
        }


        private static IAttributeModel ProcessPath(List<IAttributeStep> steps, IObjectInstance ownerInstance)
        {
            foreach (IAttributeStep step in steps)
            {
                step.Attribute = ownerInstance.GetAttribute(step.Name);
                IAttributeModel attribute = step.Attribute;

                if (steps.Count > 1 && attribute is IReferenceAttribute)
                {
                    //TODO: rewrite this
                    List<IAttributeStep> tempSteps = new List<IAttributeStep>();
                    for(int i = steps.IndexOf(step)+1; i < steps.Count; i++)
                    {
                        tempSteps.Add(steps[i]);
                    }
                    return PathProcessor.ProcessPath(tempSteps, ((IReferenceAttribute)attribute).Field.Reference);
                }
                else
                {
                    return attribute;
                }
            }
            return null;
        }
    }
}
