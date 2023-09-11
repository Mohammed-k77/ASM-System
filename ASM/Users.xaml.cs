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
using WpfApp.Messages;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        ProjectEntities po = new ProjectEntities();
        private int selctid;
        public Users()
        {
            InitializeComponent();
         
        }

        public void filldata()
        {
            dgvUsers.ItemsSource = po.Employees.Select(x => new
            {
                User_id=x.User_id,
                Name =x.Name,
                Position=x.Position,
                Phone_num=x.Phone_num,
                Address=x.Address,
                Salary=x.Salary,
                Emile = x.Email,
               Date_of_employment=x.Date_of_employment
            }).ToList();
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            filldata();
        }

        private void dgvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUsers.SelectedItem==null)
            {
                return;
            }
            Employee selctuser = new Employee();
            var dg = dgvUsers.SelectedItem;
            selctid = Convert.ToInt32((dgvUsers.SelectedCells[0].Column.GetCellContent(dg) as TextBlock).Text);
            selctuser = po.Employees.Where(x => x.User_id == selctid).FirstOrDefault();
            txtName.Text = selctuser.Name;
            txtSalary.Text = selctuser.Salary.ToString();
            txtaddress.Text = selctuser.Address;
            txtpone_num.Text = selctuser.Phone_num;
            txtEmil.Text = selctuser.Email;
            txtPosition.Text = selctuser.Position;
            txtDate.SelectedDate = selctuser.Date_of_employment;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Employee newuser = new Employee();
            newuser.Name = txtName.Text;
            newuser.Address = txtaddress.Text;
            newuser.Phone_num = txtpone_num.Text;
            newuser.Position = txtPosition.Text;
            newuser.Salary = decimal.Parse(txtSalary.Text);
            newuser.Email = txtEmil.Text;
            newuser.Date_of_employment = txtDate.DisplayDate.Date;
           
                po.Employees.Add(newuser);
                po.SaveChanges();
                filldata();
                clear();
            }
            catch (Exception )
            {
                if (txtName.Text == null || txtSalary.Text == null
                   || txtaddress.Text == null ||
                   txtpone_num.Text != null || txtEmil.Text == null || txtPosition.Text == null)
                {
                    MessageOk messageOk = new MessageOk("تنبيه ........ ",
                                                    "الرجاء ملأ الحقول",
                                                    @"/Images/information.png", @"/Images/askquestion.png", "b");
                    messageOk.ShowDialog();
                }
                else
                {
                    MessageOk messageOk = new MessageOk("تنبيه ........ ",
                                                                   "يوجد خطأ في أدخال الحقول تأكد من الادخال بالشكل الصحيح",
                                                                   @"/Images/information.png", @"/Images/askquestion.png", "b");
                }
            }       
        }
                  
        private void clear()
        {
            txtName.Text = "";
            txtSalary.Text = "";
            txtaddress.Text = "";
            txtpone_num.Text = "";
            txtEmil.Text = "";
            txtPosition.Text = "";
            txtDate.SelectedDate = null;
        }
      
        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            Employee ediuser = po.Employees.Find(selctid);

            if (ediuser !=null)
            {
                ediuser.Name = txtName.Text;
                ediuser.Address = txtaddress.Text;
                ediuser.Phone_num = txtpone_num.Text;
                ediuser.Position = txtPosition.Text;
                ediuser.Salary = decimal.Parse(txtSalary.Text);
                ediuser.Email = txtEmil.Text;
                ediuser.Date_of_employment = txtDate.DisplayDate;
                try
                {
                    po.SaveChanges();
                    filldata();
                    clear();    
                }
                catch (Exception )
                {
                    throw;

                }
            }
           

        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            Employee deluser = po.Employees.Find(selctid);

            if (deluser != null)
            {
                po.Employees.Remove(deluser);
                try
                {
                    po.SaveChanges();
                    filldata();
                    clear();

                }
                catch (Exception)
                {
                    MessageOk messageOk = new MessageOk("تنبيه ........ ", "الموظف لديه حساب نشط لايمكنك الحدف ... أذا أردت حدفه قم بحدف حسابه أولا", @"/Images/information.png", @"/Images/askquestion.png", "o");
                    messageOk.ShowDialog();
                }
            }
      
        }


        private void btnNwe_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

    }
}
