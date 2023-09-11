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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Acounts.xaml
    /// </summary>
    public partial class Acounts : UserControl
    {
        ProjectEntities po = new ProjectEntities();
        private int selctid;
        public Acounts()
        {
            InitializeComponent();
        }

        public void filldata()
        {

            dgvAcounts.ItemsSource = null;
            dgvAcounts.ItemsSource = po.Acounts.Select(x => new AcountsVM
            {
                Acount_id = x.Acount_id,
                User_id = x.User_id,
                User_name = x.User_name,
                Employ_name = x.Employee.Name,
                Password = x.Password

            }).ToList();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEmployName.ItemsSource = po.Employees.Select(x => new { x.User_id, x.Name }).ToList();
            filldata();
        }

        private void dgvAcounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgvAcounts.SelectedItem == null)
                    return;
                var dg = (AcountsVM)dgvAcounts.SelectedItem;
                selctid = dg.Acount_id;
                txtUserName.Text = dg.User_name;
                txtPassword.Text = dg.Password.ToString();
                cmbEmployName.SelectedValue = dg.User_id;

            }
            catch (Exception)
            {

            }
 
            
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Acount newacount = new Acount();
            newacount.User_id =(int)cmbEmployName.SelectedValue;
            newacount.User_name = txtUserName.Text;
            newacount.Password = int.Parse(txtPassword.Text);

            try
            {
                po.Acounts.Add(newacount);
                po.SaveChanges();
                filldata();
                clear();


            }
            catch (Exception )
            {

            }
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            Acount editacont = po.Acounts.Find(selctid);
            if (editacont != null)
            {
                editacont.User_name = txtUserName.Text;
                editacont.User_id =(int) cmbEmployName.SelectedValue;
                editacont.Password = int.Parse(txtPassword.Text);
                try
                {
                    po.SaveChanges();
                    filldata();
                    clear();

                }
                catch (Exception)
                {
                    throw;
                }
            }
           

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Acount delacont = po.Acounts.Find(selctid);
            if (delacont != null)
            {
                po.Acounts.Remove(delacont);
                try
                {
                    po.SaveChanges();
                    filldata();
                    clear();

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }


        private void clear()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            cmbEmployName.SelectedValue = null;
        }

        
    }
}
