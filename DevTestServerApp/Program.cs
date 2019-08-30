using INVex.ORM.Common;
using INVex.ORM.DataBase.SQLServer;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Holders;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Modify;
using System;
using System.IO;

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

            ObjectModel model = new ObjectModel(1, "test", File.ReadAllText(string.Format(@"{0}/Template.xml", AppDomain.CurrentDomain.BaseDirectory)), Constants.DefaultInstanceType);
            ObjectInstanceXML instance = new ObjectInstanceXML(model);

            instance.SetAttributeValue(new AttributePath(new AttributeStep("Id")), 4);

            DbConnectionHolder.Current.CreateConnection(new SQLServerConnection(@"Data Source=192.168.1.45\SQLWIFI,1433;Initial Catalog=INVex_ORM;User=sa;Password=Paper;Integrated security=false;"));

            ObjectModelsHolder.Current.LoadModels(typeof(ObjectModel));

            object test = ObjectModelsHolder.Current.CreateInstance("Model");

            //RawQuery rawQuery = new RawQuery("SELECT * FROM T_BaseObjects");
            //SQLExecutionResult result = (SQLExecutionResult)rawQuery.Execute();

            Console.ReadKey();
        }
    }
}
