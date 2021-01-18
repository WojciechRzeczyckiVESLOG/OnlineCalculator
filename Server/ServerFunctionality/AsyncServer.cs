
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using ServerFunctionality;
using System.Diagnostics;
using DatabaseConnection;

namespace ServerFunctionality
{
    /// <summary>
    /// This class implements the TCP Server as a child object of the abstract server.
    /// </summary>
    public class AsyncServer : AbstractServer
    {
        DBConnect database = new DBConnect();
        //bool important = true;
        Regex regex = new Regex(@"\d+");
        Random rnd = new Random();
        public delegate void TransmissionDataDelegate(NetworkStream nStream);
        Parser parser = new Parser();


        public AsyncServer(IPAddress IP, int port) : base(IP, port)
        {
        }
        protected override void AcceptClient()
        {
            while (true)
            {
                tcpClient = TcpListener.AcceptTcpClient();
                stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, tcpClient);
            }
        }

        private void login(NetworkStream netStream)
        {
            byte[] buffer = new byte[128];
            byte[] bufferSend = new byte[128];
            //NetworkStream netStream = client.GetStream();

            String[] text;

            string sendMessage = "";
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

                        text = (Encoding.ASCII.GetString(buffer, 0, message_length)).Split(' ');

                        database.SetUser(text.ElementAt(1), text.ElementAt(2));

                        if (text[0] == "reg")
                        {
                            database.Check();
                            sendMessage = database.message;
                            if (sendMessage == "ACK")
                            {
                                sendMessage = "NACK";
                            }
                            else if (sendMessage == "NACK")
                            {
                                database.Insert(text[1], text[2]);
                                sendMessage = "ACK";
                            }
                        }
                        else if (text[0] == "log")
                        {
                            database.Check();
                            sendMessage = database.message;
                        }

                        netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                        Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                        System.Threading.Thread.Sleep(500);

                        if (sendMessage == "ACK")
                        {
                            database.Update();
                            break;
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("Client disconnected");
                    break;

                }

            }
            System.Threading.Thread.Sleep(2000);
        }
        protected override void BeginDataTransmission(NetworkStream netStream)
        {

            login(netStream);

            byte[] buffer = new byte[128];
            byte[] bufferSend = new byte[128];
            //NetworkStream netStream = client.GetStream();

            string sendMessage = "";
            string mess = "";
            bool logoutfromapp = false;
            Stopwatch stopWatch = new Stopwatch();
            bool x = true;
            double ts;
            while (logoutfromapp == false)
            {
                int message_length = -1;

                try
                {
                    message_length = netStream.Read(buffer, 0, buffer.Length);
                    //if to ignore "enter" key
                    if (Encoding.ASCII.GetString(buffer, 0, message_length) != "\r\n" || message_length < 0)
                    {
                        mess = Encoding.ASCII.GetString(buffer, 0, message_length);
                        if (x == true)
                        {
                            x = false;
                        }
                        else
                        {
                            stopWatch.Start();
                            ts = stopWatch.Elapsed.TotalSeconds;
                            Console.WriteLine("DUPA\n" + ts);

                            if (ts > 30)
                            {
                                mess = "LOGOUT";
                            }
                        }

                        stopWatch.Restart();


                        if (mess == "LOGOUT")
                        {

                            Console.WriteLine($"Ilosc odebranych znakow: ({Encoding.ASCII.GetString(buffer, 0, message_length)}): {message_length}");
                            logoutfromapp = true;

                            database.Logout();
                            Console.WriteLine("DUPA DUPA \n");

                            sendMessage = mess;

                            netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                            Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine($"Ilosc odebranych znakow: ({Encoding.ASCII.GetString(buffer, 0, message_length)}): {message_length}");
                            parser.setUserInput(mess);

                            database.AddHistory(mess);

                            sendMessage = parser.execute().ToString();



                            netStream.Write(Encoding.ASCII.GetBytes(sendMessage), 0, sendMessage.Length);
                            Console.WriteLine($"Ilosc wyslanych znakow ({sendMessage}): {sendMessage.Length}");
                            System.Threading.Thread.Sleep(500);
                        }
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
        private void TransmissionCallback(IAsyncResult ar)
        {
        }
        /// <summary>
        /// Overrided comment.
        /// </summary>
        public override void Start()
        {
            StartListening();
            //transmission starts within the accept function
            AcceptClient();
        }
    }
}
