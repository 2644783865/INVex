using INVex.ORM.Objects;
using INVex.ORM.Objects.Attributes;
using INVex.ORM.Objects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTestServerApp
{
    public class ChatMessage : ObjectInstanceXML
    {
        public static string _Name { get; } = "ChatMessage";

        public ChatMessage()
        {
        }

        public ChatMessage(IObjectModel model) : base(model)
        {
        }

        public static AStep sName { get { return new AStep("Name"); } }
        public static AStep sChatId { get { return new AStep("ChatId"); } }
        public static AStep sTime { get { return new AStep("Time"); } }
    }
}
