using INVex.ORM.Objects;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTestServerApp
{
    public class User : ObjectInstanceXML
    {

        public static string ModelName => "User";

        public User(IObjectModel model) : base(model)
        {

        }

        public string Name
        {
            get
            {
                return (string)this.GetAttribute("Name").Field.Value;
            }
            set
            {
                this.SetAttributeValue("Name", value);
            }
        }

        public Guid Guid
        {
            get
            {
                return (Guid)this.GetAttribute("Guid").Value;
            }
            set
            {
                this.SetAttributeValue("Guid", value);
            }
        }
    }
}
