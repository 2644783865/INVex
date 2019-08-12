using INVex.Core.Networking.Base;
using INVex.Core.Networking.Server;
using INVex.Core.Objects;
using INVex.Core.Serialize;
using INVex.Core.Serialize.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DevTestServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleAsyncListener.StartListening();
        }
    }
}
