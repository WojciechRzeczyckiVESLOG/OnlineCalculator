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
                client.SendMessage(("log " + logField.Text + " " + passField.Text));
                client.TryReceive();

            }
            catch (SocketException)
            {

            }

            if (client.receivedMessage == "ACK")
            {
                this.Hide();
                CalcWindow window = new CalcWindow(client);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("login and password don't match.", "ERROR");
            }

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
                //comment this line to test
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
                client.SendMessage(("reg " + logField.Text + " " + passField.Text));
                client.TryReceive();

            }
            catch (SocketException)
            {

            }

            if (client.receivedMessage == "ACK")
            {
                this.Hide();
                CalcWindow window = new CalcWindow(client);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("This login is already taken", "ERROR");
            }

            client.receivedMessage = "";

        }
    }
}
