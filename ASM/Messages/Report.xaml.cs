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
using WpfApp.Model;
using WpfApp.Messages;

namespace WpfApp.Messages
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        ProjectEntities po = new ProjectEntities();
        int unExitid;

        public Report(int unid)
        {
            InitializeComponent();
            unExitid = unid;
            txtReport.Focus();
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
                Focus();
                Unauthorizeed_exit Un = po.Unauthorizeed_exit.Find(unExitid);
                if (Un != null)
                {
                    Un.Information = txtReport.Text;
                }
                try
                {
                    po.SaveChanges();
                    MainWindow.instance.Exitwin.filldata();
                    MainWindow.instance.Notification();
                    this.Close();
                }
                catch (Exception)
                {
                    throw;
                }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnOk.Focus();
            }
        }
    }
}
