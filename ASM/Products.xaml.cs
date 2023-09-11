using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfApp.Messages;
using WpfApp.Model;
using WpfApp.ViewModel;
using System.Web;
using System.Reflection;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {  
        ProjectEntities po = new ProjectEntities();
        private int selctid;
        string fileName;
        string path;
        public Products()
        {
            InitializeComponent();
           
        }



        public void filldata()
        {

            dgvProducts.ItemsSource = po.Products.Select(x => new ProductsInfoVM
            {
                Pro_id=x.Pro_id,
                Cateqorize_id = x.Categorize_Id,
                Classification_id = x.Calssification_Id,
                made_in_id = x.made,
                Name =x.Name,
                price=x.Price,
                Weight=x.Weight,
                pure_weight=x.PureWeight,
                Karat=x.Karat,
                Serial_number= x.Serial_number,
                Cateqorize=x.Cateqorize.Name,
                Classification=x.Classification.Name,
                Imported_Quntity=x.Imported_Quantity,
                made_in=x.Cuntry.Name,
                ImageUrl=x.ImageUrl,    
            }).ToList();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCatogrize.ItemsSource = po.Cateqorizes.Select(x => new {x.Id , x.Name }).ToList();
            cmbclassification.ItemsSource = po.Classifications.Select(x => new { x.Id, x.Name }).ToList();
            cmbMade_in.ItemsSource = po.Cuntries.Select(x => new { x.Id, x.Name }).ToList();
            filldata();
        }

        private void btnproinfo_Click(object sender, RoutedEventArgs e)
        {
            btnproinfo.Visibility = Visibility.Hidden;
            btnproinfoClose.Visibility = Visibility.Visible;
            dgvProducts.Columns[6].Visibility = Visibility.Visible;
            dgvProducts.Columns[7].Visibility = Visibility.Visible;
            dgvProducts.Columns[8].Visibility = Visibility.Visible;
            dgvProducts.Columns[9].Visibility = Visibility.Visible;
            dgvProducts.Columns[10].Visibility = Visibility.Visible;
            stkCatogrize.Visibility = Visibility.Visible;
            stkclassification.Visibility = Visibility.Visible;
            stkImported_Quntity.Visibility = Visibility.Visible;
            stkMade_in.Visibility = Visibility.Visible;
            stkSerial_num.Visibility = Visibility.Visible;
            ProductAmg.Height = 150;
            ProductAmg.Width = 180;
        }

        private void btnproinfoClose_Click(object sender, RoutedEventArgs e)
        {
            btnproinfo.Visibility = Visibility.Visible;
            btnproinfoClose.Visibility = Visibility.Hidden;
            dgvProducts.Columns[6].Visibility = Visibility.Hidden;
            dgvProducts.Columns[7].Visibility = Visibility.Hidden;
            dgvProducts.Columns[8].Visibility = Visibility.Hidden;
            dgvProducts.Columns[9].Visibility = Visibility.Hidden;
            dgvProducts.Columns[10].Visibility = Visibility.Hidden;
            stkCatogrize.Visibility = Visibility.Hidden;
            stkclassification.Visibility = Visibility.Hidden;
            stkImported_Quntity.Visibility = Visibility.Hidden;
            stkMade_in.Visibility = Visibility.Hidden;
            stkSerial_num.Visibility = Visibility.Hidden;
            ProductAmg.Height = 180;
            ProductAmg.Width = 380;
        }


        private void dgvProducts_info_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgvProducts.SelectedItem==null)
                return;

                var dg = (ProductsInfoVM)dgvProducts.SelectedItem;
                selctid = dg.Pro_id;
                txtName.Text = dg.Name;
                txtPeice.Text = dg.price.ToString();
                txtKerat.Text = dg.Karat.ToString();
                txtPur_w.Text = dg.pure_weight;
                cmbCatogrize.SelectedValue = dg.Cateqorize_id; 
                txtImported_Quntity.Text = dg.Imported_Quntity;
                cmbMade_in.SelectedValue = dg.made_in_id;
                cmbclassification.SelectedValue = dg.Classification_id;
                txtSerial_num.Text = dg.Serial_number.ToString();
                txtWight.Text = dg.Weight;
                ProductAmg.Source = new BitmapImage(new Uri(@"/Upload/"+dg.ImageUrl, UriKind.RelativeOrAbsolute));
            }
            catch (Exception )
            {
                        
            }            
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            Product newpoduct = new Product();
            newpoduct.Name = txtName.Text;
            newpoduct.Price = decimal.Parse(txtPeice.Text);
            newpoduct.Karat = txtKerat.Text;
            newpoduct.Weight = txtWight.Text;
            newpoduct.PureWeight = txtWight.Text;
            newpoduct.Serial_number = txtSerial_num.Text;
            newpoduct.Categorize_Id = (int)cmbCatogrize.SelectedValue;
            newpoduct.Calssification_Id = (int)cmbclassification.SelectedValue;
            newpoduct.made = (int)cmbMade_in.SelectedValue;
            newpoduct.Imported_Quantity = txtImported_Quntity.Text;
           /* if(fileName !="")
            {
                string fullRootPath = Environment.CurrentDirectory;
                string RootPath = fullRootPath.Remove(fullRootPath.Length - 9);
                File.Copy(path, RootPath + @"Upload\" + fileName);
                newpoduct.ImageUrl = fileName;
            }*/
            try
            {
                po.Products.Add(newpoduct);
                po.SaveChanges();
                filldata();
                clear();

            }
            catch (Exception ex )
            {
                MessageOk messageOk = new MessageOk("تنبيه ........ ", ex.ToString()+"يوجد خطأ في أدخال الحقول تأكد من الادخال بالشكل الصحيح", @"/Images/information.png", @"/Images/askquestion.png","g");
                messageOk.ShowDialog();
            }

        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
           
            Product pro = po.Products.Find(selctid);

            if (pro != null)
            {
                pro.Name = txtName.Text;
                pro.Price = decimal.Parse(txtPeice.Text);
                pro.Karat = txtKerat.Text;
                pro.Weight = txtWight.Text;
                pro.PureWeight = txtPur_w.Text;
                pro.Serial_number =txtSerial_num.Text;
                pro.Categorize_Id =(int) cmbCatogrize.SelectedValue;
                pro.Calssification_Id = (int)cmbclassification.SelectedValue;
                pro.made = (int)cmbMade_in.SelectedValue;
                pro.Imported_Quantity = txtImported_Quntity.Text;
                string oldPicture = pro.ImageUrl;
                string newPicture = fileName;
               /* if(fileName != "")
                {
                    if(oldPicture != newPicture)
                        {
                            string fullRootPath = Environment.CurrentDirectory;
                            string RootPath = fullRootPath.Remove(fullRootPath.Length - 9);
                            File.Delete(RootPath + @"Upload\" + oldPicture);
                            File.Copy(path, RootPath + @"Upload\" + fileName);
                            pro.ImageUrl = newPicture;  
                        }
                } */            
            } 
           

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

        private void clear()
        {
            txtName.Text = "";
            txtPeice.Text = "";
            txtKerat.Text = "";
            txtPur_w.Text = "";
            txtImported_Quntity.Text = "";
            txtSerial_num.Text = "";
            txtWight.Text = "";
            cmbCatogrize.SelectedValue = null;
            cmbclassification.SelectedValue = null;
            cmbMade_in.SelectedValue = null;
        }

        private void btnNwe_Click(object sender, RoutedEventArgs e)
        {
            btnproinfo.Visibility = Visibility.Hidden;
            btnproinfoClose.Visibility = Visibility.Visible;
            dgvProducts.Columns[6].Visibility = Visibility.Visible;
            dgvProducts.Columns[7].Visibility = Visibility.Visible;
            dgvProducts.Columns[8].Visibility = Visibility.Visible;
            dgvProducts.Columns[9].Visibility = Visibility.Visible;
            dgvProducts.Columns[10].Visibility = Visibility.Visible;
            stkCatogrize.Visibility = Visibility.Visible;
            stkclassification.Visibility = Visibility.Visible;
            stkImported_Quntity.Visibility = Visibility.Visible;
            stkMade_in.Visibility = Visibility.Visible;
            stkSerial_num.Visibility = Visibility.Visible;
            ProductAmg.Height = 150;
            ProductAmg.Width = 180;
            clear();
            filldata();
        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            
            Product delpro = po.Products.Find(selctid);
            if (delpro != null)
            {
                po.Products.Remove(delpro);
                try
                {
                    po.SaveChanges();
                    filldata();
                    clear();

                }
                catch (Exception)
                {
                    
                   
                }

            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx.Text !="")
            {           
                dgvProducts.ItemsSource = null;
                var filterlist = po.Products.Where(x=> x.Name.Contains(tbx.Text));
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            
            else
            {
                filldata();
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnproinfo.Visibility = Visibility.Hidden;
            btnproinfoClose.Visibility = Visibility.Visible;
            dgvProducts.Columns[6].Visibility = Visibility.Visible;
            dgvProducts.Columns[7].Visibility = Visibility.Visible;
            dgvProducts.Columns[8].Visibility = Visibility.Visible;
            dgvProducts.Columns[9].Visibility = Visibility.Visible;
            dgvProducts.Columns[10].Visibility = Visibility.Visible;
            stkCatogrize.Visibility = Visibility.Visible;
            stkclassification.Visibility = Visibility.Visible;
            stkImported_Quntity.Visibility = Visibility.Visible;
            stkMade_in.Visibility = Visibility.Visible;
            stkSerial_num.Visibility = Visibility.Visible;
            ProductAmg.Height = 150;
            ProductAmg.Width = 180;
            if (cmbCatogrize.SelectedItem != null && cmbclassification.SelectedItem == null && cmbMade_in.SelectedItem == null)
            {
                int Categorize_id = Convert.ToInt32(cmbCatogrize.SelectedValue);
                var filterlist = po.Products.Where(x => x.Categorize_Id == Categorize_id);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else if (cmbclassification.SelectedItem != null && cmbMade_in.SelectedItem == null && cmbCatogrize.SelectedItem == null)
            {
                int Classification_id = Convert.ToInt32(cmbclassification.SelectedValue);
                var filterlist = po.Products.Where(x => x.Calssification_Id == Classification_id);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else if (cmbMade_in.SelectedItem != null && cmbclassification.SelectedItem == null && cmbCatogrize.SelectedItem == null)
            {
                int made_in= Convert.ToInt32(cmbMade_in.SelectedValue);
                var filterlist = po.Products.Where(x => x.made == made_in);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else if (cmbCatogrize.SelectedItem != null && cmbclassification != null && cmbMade_in.SelectedItem == null)
            {
                int Classification_id = Convert.ToInt32(cmbclassification.SelectedValue);
                int Categorize_id = Convert.ToInt32(cmbCatogrize.SelectedValue);
                var filterlist = po.Products.Where(x => x.Categorize_Id == Categorize_id && x.Calssification_Id == Classification_id);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).Where(x=> x.Classification_id == Classification_id).ToList();
            }
            else if (cmbMade_in.SelectedItem != null && cmbclassification != null && cmbCatogrize.SelectedItem == null)
            {
                var filterlist = po.Products.Where(x => x.Categorize_Id == Convert.ToInt32(cmbCatogrize.SelectedValue) && x.made == Convert.ToInt32(cmbMade_in.SelectedValue));
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else if (cmbMade_in.SelectedItem != null && cmbCatogrize != null && cmbclassification.SelectedItem == null)
            {
                int made_in = Convert.ToInt32(cmbMade_in.SelectedValue);
                int Classification_id = Convert.ToInt32(cmbclassification.SelectedValue);
                var filterlist = po.Products.Where(x => x.made == made_in && x.Calssification_Id == Classification_id);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else if (cmbCatogrize.SelectedItem != null && cmbclassification != null && cmbMade_in.SelectedItem != null)
            {
                int Categorize_id = Convert.ToInt32(cmbCatogrize.SelectedValue);
                int Classification_id = Convert.ToInt32(cmbclassification.SelectedValue);
                int made_in = Convert.ToInt32(cmbMade_in.SelectedValue);
                var filterlist = po.Products.Where(x => x.Categorize_Id == Categorize_id
                && x.Calssification_Id == Classification_id && x.made == made_in);
                dgvProducts.ItemsSource = filterlist.Select(x => new ProductsInfoVM
                {
                    Pro_id = x.Pro_id,
                    Cateqorize_id = x.Categorize_Id,
                    Classification_id = x.Calssification_Id,
                    made_in_id = x.made,
                    Name = x.Name,
                    price = x.Price,
                    Weight = x.Weight,
                    pure_weight = x.PureWeight,
                    Karat = x.Karat,
                    Serial_number = x.Serial_number,
                    Cateqorize = x.Cateqorize.Name,
                    Classification = x.Classification.Name,
                    Imported_Quntity = x.Imported_Quantity,
                    made_in = x.Cuntry.Name
                }).ToList();
            }
            else
                filldata();
        }

        private void btnChooce_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDilog = new OpenFileDialog();
            openDilog.Filter = "Image files |*.bmp;*.jpg;*.png";
            openDilog.FilterIndex = 1;
            if (openDilog.ShowDialog()==true)
            {              
                ProductAmg.Source = new BitmapImage(new Uri(openDilog.FileName));
                path = openDilog.FileName;
                fileName = openDilog.SafeFileName;
            }
        }
    }
    }



    

