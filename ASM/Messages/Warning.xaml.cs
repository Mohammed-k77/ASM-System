using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp.Messages;

namespace WpfApp.Messages
{
    /// <summary>
    /// Interaction logic for Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        int unExitid;
        public Warning(string MessageHeader, string Message1, string Message2, string Message3, string Message4, string IconHeader, string ImageMessage, string Borderstyle, int unid)
        {
            InitializeComponent();

            btnOk.Focus();

            txtHeader.Text = MessageHeader;
            txtMessage1.Text = Message1;
            txtMessage2.Text = Message2;
            txtMessage3.Text = Message3;
            txtMessage4.Text = Message4;
            unExitid = unid;

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

        private void btnAddinfo_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report(unExitid);
            report.ShowDialog();
            this.Close();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
