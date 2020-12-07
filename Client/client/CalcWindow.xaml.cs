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

        private void keyboardHandler(object sender, KeyEventArgs e)
        {
            if (WPFequation.Text == "Enter your favourite equation . . .") WPFequation.Text = "";

            if (e.Key == Key.NumPad0 || e.Key == Key.D0)
            {
                equationText += "0";
                WPFequation.Text += "0";
            }
            if (e.Key == Key.NumPad1 || e.Key == Key.D1)
            {
                equationText += "1";
                WPFequation.Text += "1";
            }
            if (e.Key == Key.NumPad2 || e.Key == Key.D2)
            {
                equationText += "2";
                WPFequation.Text += "2";
            }
            if (e.Key == Key.NumPad3 || e.Key == Key.D3)
            {
                equationText += "3";
                WPFequation.Text += "3";
            }
            if (e.Key == Key.NumPad4 || e.Key == Key.D4)
            {
                equationText += "4";
                WPFequation.Text += "4";
            }
            if (e.Key == Key.NumPad5 || e.Key == Key.D5)
            {
                equationText += "5";
                WPFequation.Text += "5";
            }
            if (e.Key == Key.NumPad6 || e.Key == Key.D6)
            {
                equationText += "6";
                WPFequation.Text += "6";
            }
            if (e.Key == Key.NumPad7 || e.Key == Key.D7)
            {
                equationText += "7";
                WPFequation.Text += "7";
            }
            if (e.Key == Key.NumPad8 || e.Key == Key.D8)
            {
                equationText += "8";
                WPFequation.Text += "8";
            }
            if (e.Key == Key.NumPad9 || e.Key == Key.D9)
            {
                equationText += "9";
                WPFequation.Text += "9";
            }
            if (e.Key == Key.Add)
            {
                equationText += "+";
                WPFequation.Text += "+";
            }
            if (e.Key == Key.Subtract)
            {
                equationText += "-";
                WPFequation.Text += "-";
            }
            if (e.Key == Key.Multiply)
            {
                equationText += "*";
                WPFequation.Text += "*";
            }
            if (e.Key == Key.Divide)
            {
                equationText += "/";
                WPFequation.Text += "/";
            }
            if (e.Key == Key.Enter)
            {
                buttonEqualizeClicked(sender, e);
            }
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
