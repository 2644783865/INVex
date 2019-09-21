using INVex.ORM.Common;
using INVex.ORM.DataBase.SQLServer;
using INVex.ORM.Expressions.Base;
using INVex.ORM.Expressions.Logical;
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
                RequiredAttributes = new List<IAttributePath>
                {
                    new APath(new AStep("Message")),
                    new APath(new AStep("Name")),
                    new APath(new AStep("ChatId"))
                }
            };
            List<IObjectInstance> messages = qr.Execute();

            qr = new ObjectQuery(User.ModelName)
            {
                RequiredAttributes = new List<IAttributePath>
                {
                    new APath(new AStep("Name"))
                }
            };
            List<User> users = qr.Execute<User>();

            foreach(IObjectInstance singleMessage in messages)
            {
                Console.WriteLine(singleMessage.GetAttribute("Message").Value);
            }

            bool criteriaResult =
                new Criteria(
                    CriteriaType.AND,
                    new AttributeCondition
                    (
                        messages[4].GetAttribute("Name"),
                        users[0].GetAttribute("Name"),
                        OperatorType.Equal
                    ),
                    new AttributeCondition
                    (
                        messages[4].GetAttribute("Message"),
                        users[0].GetAttribute("Name"),
                        OperatorType.NotEqual
                    )
                ).IsTrue();

            users[0].SetAttributeValue("Password", "test");
            users[0].Save();

            User test = (User)ObjectInstance.CreateInstance(users[0].Model);
            test.Guid = Guid.NewGuid();
            test.Name = "TESTNAME";
            test.Save();

            Console.ReadKey();
        }
    }
}
