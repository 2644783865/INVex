using INVex.ORM.Common;
using INVex.ORM.DataBase.SQLServer;
using INVex.ORM.Expressions.Base;
using INVex.ORM.Expressions.Logical;
using INVex.ORM.Expressions.Modify;
using INVex.ORM.Expressions.Queries;
using INVex.ORM.Holders;
using INVex.ORM.Holders.Modify;
using INVex.ORM.Objects;
using INVex.ORM.Objects.Attributes;
using INVex.ORM.Objects.Attributes.Base;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.IO;

namespace DevTestServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectModel model = new ObjectModel(1, "test", File.ReadAllText(string.Format(@"{0}/Template.xml", AppDomain.CurrentDomain.BaseDirectory)), Constants.DefaultInstanceType);
            ObjectInstanceXML instance = new ObjectInstanceXML(model);

            instance.SetAttributeValue(new APath(new AStep("Id")), 4);

            DbConnectionHolder.Current.CreateConnection(new SQLServerConnection(@"Data Source=192.168.1.45\SQLWIFI,1433;Initial Catalog=INVex_ORM;User=sa;Password=Paper;Integrated security=false;"));
            ObjectModelsHolder.Current.SetupHolder(new DefaultObjectsHolder("def"));
            ObjectModelsHolder.Current.Holder.LoadModels();

            ObjectQuery qr = new ObjectQuery("ChatMessage")
            {
                ReturnedAttributes = new List<IAttributePath>
                {
                    new APath(new AStep("Name")),
                    new APath(new AStep("ChatId"))
                },
                OrderBy = new APath(new AStep("Time"))
            };
            List<IObjectInstance> messages = qr.Execute();

            qr = new ObjectQuery(User.ModelName)
            {
                ReturnedAttributes = new List<IAttributePath>
                {
                    new APath(new AStep("Name"))
                }
            };
            List<User> users = qr.Execute<User>();

            foreach(IObjectInstance singleMessage in messages)
            {
                Console.WriteLine(singleMessage.GetAttribute("Message").Value);
            }

            users[0].SetAttributeValue("Password", "test");
            users[0].Save();

            User test = (User)ObjectInstance.CreateInstance(users[0].Model);
            test.Guid = Guid.NewGuid();
            test.Name = "TESTNAME";
            test.Save();

            ObjectInstanceXML xmlInst = (ObjectInstanceXML)ObjectInstance.GetInstance("Order", "B77E47AE-8F2E-4C33-9D49-9D8E1C620451");

            bool criteriaResult =
                new Criteria(
                    CriteriaType.AND,
                    new ValueCondition
                    (
                        xmlInst.GetAttribute("OrderNum"),
                        0,
                        OperatorType.NotEqual
                    )
                ).IsTrue();

            Console.ReadKey();
        }
    }
}
