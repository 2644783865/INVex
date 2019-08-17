using INVex.ORM.Objects;
using INVex.ORM.Objects.Modify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace DevTestServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //AssemblyUtils.ApplicationAssemblyFullName = Assembly.GetExecutingAssembly().FullName;
            //new CommandsHolder().Register();

            //AutoCommandProcessor.RegisterProcessors();
            //Console.WriteLine(HoldersCollection.Current[CommandsHolder.HolderName].Name);

            //SimpleAsyncListener.StartListening();

            ObjectModel model = new ObjectModel(1, "test", File.ReadAllText(string.Format(@"{0}/Template.xml", AppDomain.CurrentDomain.BaseDirectory)));
            ObjectInstanceXML instance = new ObjectInstanceXML(model);

            instance.SetAttributeValue(new AttributePath(new AttributeStep("Id")), 4);


            Console.ReadKey();
        }
    }
}
