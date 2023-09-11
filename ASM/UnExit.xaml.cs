using System;
using System.Collections;
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
using System.IO.Ports;
using WpfApp.Messages;
using WpfApp.Model;
using WpfApp.ViewModel;

namespace WpfApp
{
    public partial class UnExit : Window
    {
        public static UnExit instance;
        string aa = "", showUser;
        int id;
        public static ArrayList  Tags = new ArrayList();
        SerialPort sp = new SerialPort();
        ProjectEntities po = new ProjectEntities();
        public UnExit()
        {
            InitializeComponent();
            instance = this;
        }

        public void incomingString(String val, string user)
        {
            showUser = user;
            int len = val.Length;
            aa = val.Remove(len - 1);
            cheek(aa);
        }

        private void cheek(string incomString)
        {


            for (int i=0;i<Tags.Count;i++)
            {
                if (Tags[i].ToString() == incomString)
                {
                    Product pro = po.Products.FirstOrDefault(x => x.Serial_number == incomString);
                    Unauthorizeed_exit Ex = po.Unauthorizeed_exit.FirstOrDefault(x => x.Pro_id == pro.Pro_id
                    && x.Date.Month == DateTime.Now.Month && x.Date.Day == DateTime.Now.Day
                    && x.Date.Hour == DateTime.Now.Hour && x.Date.Minute == DateTime.Now.Minute);
                    if (Ex != null)
                    {
                        id = Ex.Id;
                        Edit(incomString);
                        reFill(incomString);
                        return;
                    }
                    else Add(incomString);
                    reFill(incomString);
                    return;
                }
            }
                Add(incomString);
                Tags.Add(incomString);
                reFill(incomString);
        }

        private void reFill(string incomString)
        {
            Product filterlist = po.Products.FirstOrDefault(x => x.Serial_number == incomString);
            Classification clas = po.Classifications.FirstOrDefault(x => x.Id == filterlist.Calssification_Id); ;
            Cateqorize cate = po.Cateqorizes.FirstOrDefault(x => x.Id == filterlist.Categorize_Id);
            Cuntry cant = po.Cuntries.FirstOrDefault(x => x.Id == filterlist.made);
            if (filterlist == null)
                return;
            txtName.Text = filterlist.Name;
            txtKerat.Text = filterlist.Karat;
            txtPeice.Text = filterlist.Price.ToString();
            txtSerial_No.Text = filterlist.Serial_number;
            txtWight.Text = filterlist.Weight;
            txtCatogrize.Text = cate.Name;
            txtclassification.Text = clas.Name;
            txtPur_w.Text = filterlist.PureWeight;
            txtMade_in.Text = cate.Name;
            txtQuntity.Text = "1";
            txtUsername.Text = showUser;
            txtDoor_No.Text = "1";
            amgProducts.Source = new BitmapImage(new Uri(@"/Upload/"+filterlist.ImageUrl, UriKind.RelativeOrAbsolute));
            filldata();         
        }

        private void Add(String val)
        {
            Unauthorizeed_exit Un = new Unauthorizeed_exit();
            Product filterlistP = po.Products.FirstOrDefault(x => x.Serial_number == val);
            Employee filterlistE= po.Employees.FirstOrDefault(x => x.Name == showUser);
            Un.Door_num = 1;
            Un.User_id = filterlistE.User_id;
            Un.Pro_id = filterlistP.Pro_id;
            Un.Quantity = 1;
            Un.Information = "";
            Un.Date = System.DateTime.Now;
            try
            {
                po.Unauthorizeed_exit.Add(Un);
                po.SaveChanges();
                filldata();
            }
            catch (Exception )
            {
               MessageOk messageOk = new MessageOk("تنبيه ........ ", "يوجد خطأ في أدخال الحقول تأكد من الادخال بالشكل الصحيح", @"/Images/information.png", @"/Images/askquestion.png", "g");
               messageOk.ShowDialog();
            }
        }

        private void Edit(string val)
        {
            Unauthorizeed_exit Un = po.Unauthorizeed_exit.Find(id);
            if (Un!=null)
            { 
                Un.Quantity ++;
            }
            try
            {
                po.SaveChanges();
                filldata();
            }
            catch (Exception )
            {
                throw;
            }
        }

        private void btnStore_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.stopAlart();
        }

        public void filldata()
        {

            dgvUnExit.ItemsSource = po.Unauthorizeed_exit.Select(x => new UnExitVM
            {
                Id = x.Id,
                Pro_id = x.Pro_id ,
                User_name = x.Employee.Name,
                price = x.Product.Price,
                Weight = x.Product.Weight,
                pure_weight = x.Product.PureWeight,
                Karat = x.Product.Karat,
                Serial_number = x.Product.Serial_number,
                Cateqorize = x.Product.Cateqorize.Name,
                Classification = x.Product.Classification.Name,
                Date=x.Date,
                Door_num=x.Door_num,
                Quantity=x.Quantity,
                made_in=x.Product.Cuntry.Name,               
                Proname=x.Product.Name
            }).ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.stopAlart();
            instance.Close();
        }

        private void dgvUnExit_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
                try
                {
                    if (dgvUnExit.SelectedItem==null)
                      return;
                    var dg = (UnExitVM)dgvUnExit.SelectedItem;
                    Product filterlist = po.Products.FirstOrDefault(x => x.Pro_id == dg.Pro_id);
                    amgProducts.Source = new BitmapImage(new Uri(@"/Upload/" + filterlist.ImageUrl, UriKind.RelativeOrAbsolute));

                 }

                catch (Exception )
                {


                }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Product filterlist = po.Products.FirstOrDefault(x => x.Serial_number == aa);
            Classification clas = po.Classifications.FirstOrDefault(x=>x.Id == filterlist.Calssification_Id);
            Cateqorize cate = po.Cateqorizes.FirstOrDefault(x => x.Id==filterlist.Categorize_Id);
            Cuntry cant = po.Cuntries.FirstOrDefault(x=> x.Id == filterlist.made);
            if (filterlist == null)
                return;
            txtName.Text = filterlist.Name;
            txtKerat.Text = filterlist.Karat;
            txtPeice.Text = filterlist.Price.ToString();
            txtSerial_No.Text = filterlist.Serial_number;
            txtWight.Text = filterlist.Weight;
            txtCatogrize.Text = cate.Name;
            txtclassification.Text = clas.Name;
            txtPur_w.Text = filterlist.PureWeight;
            txtMade_in.Text = cate.Name;
            txtQuntity.Text = "1";
            txtUsername.Text = showUser;
            txtDoor_No.Text = "1";
            filldata();
        }
    }
}
