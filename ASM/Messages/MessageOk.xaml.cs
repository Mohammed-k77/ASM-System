using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Messages
{
    /// <summary>
    /// Interaction logic for MessageOk.xaml
    /// </summary>
    public partial class MessageOk : Window
    {
        public MessageOk(string MessageHeader, string Message, string IconHeader, string ImageMessage,string Borderstyle)
        {
            InitializeComponent();

            btnOk.Focus();

            txtHeader.Text = MessageHeader;
            txtMessage.Text = Message;

            imgIconHeader.Source = new BitmapImage(new Uri(IconHeader, UriKind.RelativeOrAbsolute));
            imgMessage.Source = new BitmapImage(new Uri(ImageMessage, UriKind.RelativeOrAbsolute));
             switch (Borderstyle)
             {
                case "g":
                    Properties.Settings.Default.StyleMode = "Green";
                    //and to save the settings
                    Properties.Settings.Default.Save();
                    break;
                case "b":
                    Properties.Settings.Default.StyleMode = "Bink";
                    //and to save the settings
                    Properties.Settings.Default.Save();
                    break;
                case "o":
                    Properties.Settings.Default.StyleMode = "Orange";
                    //and to save the settings
                    Properties.Settings.Default.Save();
                    break;
            }


        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                brdrOk.Opacity = 0.7;
                DragMove();
            }
            if (e.LeftButton == MouseButtonState.Released)
            {
                brdrOk.Opacity = 1;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

