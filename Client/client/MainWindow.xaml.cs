using GUI.TCPconnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TCPclient client;
        bool isConnected = false;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void logClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                //comment these 2 lines to test offline
                client.SendMessage(("log " + logField.Text + " " + passField.Text));
                client.TryReceive();

            }
            catch (SocketException)
            {

            }

            //to test offline:
            //if(true)
                if (client.receivedMessage == "ACK")
                {
                this.Hide();
                //to test offline:
                //CalcWindow window = new CalcWindow();
                CalcWindow window = new CalcWindow(client);
                window.Show();
                
                string exitMessage = "";
                while (exitMessage != "LOGOUT" || exitMessage != "EXIT")
                {

                }

                this.Close();
            }
            else
            {
                MessageBox.Show("login and password don't match.", "ERROR");
            }

            //comment this line to test offline
            client.receivedMessage = "";

        }

        private void TcpInit(object sender, EventArgs e)
        {




        }

        private void tryConnect()
        {
            if (client != null) client.CloseConnection();
            try
            {
                //comment this line to test offline
                client = new TCPclient("127.0.0.1", 2137);
                serverStatus.Foreground = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#09EE09");
                serverStatus.Text = "Server status: Online!";
                isConnected = true;
            }
            catch (SocketException)
            {
                isConnected = false;
            }

            if (isConnected)
            {
                logButton.IsEnabled = true;
            }
            else
            {
                logButton.IsEnabled = false;
            }

        }
        private void logButtInitialized(object sender, EventArgs e)
        {

        }

        private void ReconnectClicked(object sender, RoutedEventArgs e)
        {
            tryConnect();
        }

        private void regClicked(object sender, RoutedEventArgs e)
        {

            try
            {
                //comment these 2 lines to test offline
                client.SendMessage(("reg " + logField.Text + " " + passField.Text));
                client.TryReceive();

            }
            catch (SocketException)
            {

            }

            //to test offline:
            //if (true)
                if (client.receivedMessage == "ACK")
                {
                this.Hide();
                CalcWindow window = new CalcWindow(client);
                window.Show();
                MessageBox.Show("Successfully registered! Now you can log in.");
 
            }
            else
            {
                MessageBox.Show("This login is already taken", "ERROR");
            }

            //coment this line to test offline
            client.receivedMessage = "";

        }
    }
}
