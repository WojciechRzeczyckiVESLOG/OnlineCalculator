using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GUI.TCPconnection
{
    public class TCPclient
    {
        string ipAddress;
        Int32 port;
        TcpClient client;
        NetworkStream stream;
        public string receivedMessage="";

        public TCPclient(string ipAddress, Int32 port)
        {
            //Create a TcpClient with selected ip and port and create a client stream for reading and writing
            this.ipAddress = ipAddress;
            this.port = port;
            this.client = new TcpClient(this.ipAddress, this.port);
            this.stream = client.GetStream();
            
        }


        public void SendMessage(string message)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);
        }

        public void TryReceive()
        {
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String result = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            result = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            if (result != "" && this.receivedMessage == "") this.receivedMessage = result;

        }

        public void CloseConnection()
        {
            // Close everything.
            stream.Close();
            client.Close();
        }

        static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
