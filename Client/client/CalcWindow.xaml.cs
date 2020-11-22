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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for CalcWindow.xaml
    /// </summary>

    
    public partial class CalcWindow : Window
    {
        TCPclient client;
        protected string equationText;
        public CalcWindow()
        {
            InitializeComponent();
            equationText = "";
        }

        public CalcWindow(TCPclient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void buttonClicked(object sender, RoutedEventArgs e)
        {
            if (equationText == "") WPFequation.Text = "";
            if (WPFequation.Text == "Enter your favourite equation . . .") WPFequation.Text = "";
            equationText += (sender as Button).Content.ToString();
            WPFequation.Text += (sender as Button).Content.ToString();
        }

        private void buttonEqualizeClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                client.receivedMessage = ""; 
                client.SendMessage((equationText));
                client.TryReceive();

            }
            catch (SocketException)
            {
                WPFequation.Text = "Connection Error!";
            }

            WPFequation.Text = client.receivedMessage;
            equationText = "";
        }

        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            equationText = "";
            WPFequation.Text = "Enter your favourite equation . . .";
        }


    }
}
