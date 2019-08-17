using System;
using System.IO;
using System.Xml.Linq;
using INVex.Core;
using INVex.Core.Commands;
using INVex.Core.Fields;
using INVex.Core.Networking.Client;
using INVex.Core.Objects;

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

            //SimpleClient.SendObject(model);

            SimpleAsyncClient.Connect();
            Command writeSomeText = new Command("WriteText");
            writeSomeText.Parameters.Add("Text", "Sended text");
            SimpleAsyncClient.SendCommand(writeSomeText);
            //AsyncClient.Send(model);

            Console.ReadKey();
        }
    }
}
