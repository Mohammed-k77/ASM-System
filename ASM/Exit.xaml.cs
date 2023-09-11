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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Model;
using WpfApp.ViewModel;
using WpfApp.Messages;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Exit.xaml
    /// </summary>
    public partial class Exit : UserControl
    {
        int SelectedID;
        ProjectEntities po = new ProjectEntities();
        public Exit()
        {
            InitializeComponent();
        }

        public void filldata()
        {
            this.Dispatcher.Invoke(() =>
            {
                dgvExit.ItemsSource = po.Unauthorizeed_exit.Select(x => new UnExitVM
                {
                    Id = x.Id,
                    Proname = x.Product.Name,
                    Date = x.Date,
                    Quantity = x.Quantity,
                    Door_num = x.Door_num,
                    User_name = x.Employee.Name,
                    Information = x.Information
                }).ToList();
            });
           

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            filldata();
            CheackPosition();
        }

        private void btnDellet_Click(object sender, RoutedEventArgs e)
        {
            Unauthorizeed_exit Un = po.Unauthorizeed_exit.Find(SelectedID);
            if (Un != null)
            {
                po.Unauthorizeed_exit.Remove(Un);
                try
                {
                    po.SaveChanges();
                    filldata();
                    MainWindow.instance.Notification();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
        }

  

        private void btnDelletall_Click(object sender, RoutedEventArgs e)
        {
            var all = from c in po.Unauthorizeed_exit select c;
            if (all != null)
            {
                try
                {
                    po.Unauthorizeed_exit.RemoveRange(all);
                    po.SaveChanges();
                    filldata();
                    MainWindow.instance.Notification();
                }
                catch (Exception ex)
                {

                    MessageOk message = new MessageOk("تنبيه ........ ", ex.ToString(), @"/Images/information.png", @"/Images/askquestion.png", "b");
                    message.ShowDialog();
                }

            }

        }



        public void CheackPosition()
        {
            if (MainWindow.instance.UserPosition() == true)
            {
                MangerReport.Visibility = Visibility.Visible;
                EmployeeReport.Visibility = Visibility.Hidden;
            }
            else
            {
                MangerReport.Visibility = Visibility.Hidden;
                EmployeeReport.Visibility = Visibility.Visible;
            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
                var dg = (UnExitVM)dgvExit.SelectedItem;
                SelectedID = dg.Id;

                if (dg.Information == "")
                {
                    MessageYesNo message = new MessageYesNo("؟........ ", "هل تود أسناد تقرير لهذا الخروج ", @"/Images/information.png", @"/Images/askquestion.png", "g");
                    message.ShowDialog();

                    if (message.DialogResultRetern)
                    {
                        Report report = new Report(SelectedID);
                        report.ShowDialog();
                    }
                }
        }
    }

}
