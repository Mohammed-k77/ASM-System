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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class LoginWin : Window
    {
        ProjectEntities po = new ProjectEntities();
        string username;
        public LoginWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtName.Text == "" && txtPass.Password == "")
            {
                MessageOk messageOk = new MessageOk("تنبيه ........ ", "أملأ الحقول  ..........", @"/Images/information.png", @"/Images/askquestion.png","g");
                messageOk.Owner = this;
                messageOk.ShowDialog();
            }
            else if (txtName.Text == "")
            {
                MessageOk messageOk = new MessageOk("تنبيه ........", "أدخل أسم المستخدم ..........", @"/Images/askquestion.png", @"/Images/askquestion.png", "g");
                messageOk.Owner = this;
                messageOk.ShowDialog();
            }
            else if (txtPass.Password == "")
            {
                MessageOk messageOk = new MessageOk("تنبيه ........", "أدخل كلمة المرور ..........", @"/Images/information.png", @"/Images/information.png", "g");
                messageOk.Owner = this;
                messageOk.ShowDialog();
            }

            else
            {
                Acount acc = po.Acounts.FirstOrDefault(x => x.User_name == txtName.Text && x.Password.ToString() == txtPass.Password);
                if (acc != null)
                {
                    Employee selct = po.Employees.Find(acc.User_id);
                    username = selct.Name;
                    MainWindow lo = new MainWindow(username);
                    lo.Show();
                    this.Close();
                }
                else
                {
                    MessageOk messageOk = new MessageOk("خطأ........", "أسم المستخدم او كلمة المرور خطأ ..........", @"/Images/error.png", @"/Images/error.png", "g");
                    messageOk.Owner = this;
                    messageOk.ShowDialog();
                }




            }
        }

        private void brdrLogin_Loaded(object sender, RoutedEventArgs e)
        {
            btnlogin.Focus();
            txtName.Focus();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnlogin.Focus();
            }
        }
    }
}

    

