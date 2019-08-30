using INVex.ORM.Fields;
using INVex.ORM.Objects;
using System;
using System.IO;
using System.Xml.Linq;

namespace DevTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ObjectModel model = new ObjectModel(1, "test", File.ReadAllText(string.Format(@"{0}/Template.xml", AppDomain.CurrentDomain.BaseDirectory)));            
            ObjectInstance instance = new ObjectInstance(model);

            //SimpleClient client = new SimpleClient();
            //client.Connect();

            CollectionField testColl = (CollectionField)instance.GetAttribute("Coll").Field;
            testColl.Add(new IntField(5));

            Console.ReadKey();
        }
    }
}
