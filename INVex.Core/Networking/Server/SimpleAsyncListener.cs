using INVex.Core.Commands.Base;
using INVex.Core.Serialize;
using INVex.Core.Serialize.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace INVex.Core.Networking.Server
{
    public class SimpleAsyncListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public SimpleAsyncListener()
        {
        }

        public static void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");//ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 25565);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            //state.messageSize = handler.
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);
            state.messageSize = bytesRead;
            
            if(bytesRead > 0)
            {
                if (bytesRead + state.totalBytesRead > state.messageSize)
                {
                    bytesRead = (int)state.messageSize - (int)state.totalBytesRead;
                    state.totalBytesRead = state.messageSize;
                }
                else
                {
                    state.totalBytesRead += bytesRead;
                }

                if(state.totalBytesRead >= state.messageSize)
                {
                    IBinarySerializable serializable;
                    using (MemoryStream ms = new MemoryStream(state.buffer, 0, bytesRead))
                    {
                        using (BinaryReader reader = new BinaryReader(ms))
                        {
                            serializable = SimpleSerializer.UnpackObject(reader);
                        }
                    }

                    if(serializable is ICommandBase)
                    {

                    }
                    else
                    {
                        throw new Exception("Попытка использовать объект вместо команды");
                    }
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                }
            }
        }

        public static void Send(Socket handler, IBinarySerializable serializable)
        {
            byte[] binaryData;

            using(MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    SimpleSerializer.PackObject(writer, serializable);
                }

                binaryData = ms.ToArray();
            }

            // Begin sending the data to the remote device.  
            handler.BeginSend(binaryData, 0, binaryData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
