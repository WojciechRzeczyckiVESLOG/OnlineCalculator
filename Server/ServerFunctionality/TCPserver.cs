using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerFunctionality
{
    public class TCPserver
    {
        public void run()
        {
            TcpListener tcpListen = new TcpListener(IPAddress.Parse("127.0.0.1"), 2137);
            tcpListen.Start();
            TcpClient client = tcpListen.AcceptTcpClient();

            byte[] buffer = new byte[128];
            byte[] bufferSend = new byte[128];
            NetworkStream netStream = client.GetStream();

            string sendMessage = "Calculate EVERYTHING \r\n Enter a first number: \r\n";
            netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
            while (true)
            {
                int message_length = -1;

                try
                {
                    message_length = netStream.Read(buffer, 0, buffer.Length);
                    //if to ignore "enter" key
                    if (Encoding.ASCII.GetString(buffer, 0, message_length) != "\r\n" || message_length < 0)
                    {

                        Console.WriteLine($"Ilosc odebranych znakow: ({Encoding.ASCII.GetString(buffer, 0, message_length)}): {message_length}");

                        netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                        Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                        System.Threading.Thread.Sleep(500);

                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("PuTTy zostało zamknięte");
                    break;

                }

            }
            System.Threading.Thread.Sleep(2000);



        }
    }
}
