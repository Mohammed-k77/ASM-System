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
using System.Windows.Forms;
using System.IO.Ports;
using WpfApp.Messages;
using WpfApp.Model;
using WpfApp.ViewModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Collections;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance;
        DispatcherTimer DT1 = new DispatcherTimer();
        DispatcherTimer DT2 = new DispatcherTimer();
        string Username;
        int Notficationcount;
        delegate void serialCalback(string val);
        SerialPort sp = new SerialPort();
        ProjectEntities po = new ProjectEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string username)
        {
            InitializeComponent();
            instance = this;
            lbluser.Text = username;
            Username = username;
            sp.BaudRate = 9600;
            sp.PortName = "COM3";
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
           // sp.Open();
            sp.DataReceived += Sp_DataReceived;
        }

        private void dt1_Tick(object sender, EventArgs e)
        {
            Warnning();
            DT1.Stop();
        }

        private void dt2_Tick(object sender, EventArgs e)
        {
            stopAlart();
            DT2.Stop();
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            String incomString = sp.ReadLine();
            sp.Write("1");
            DT2.Tick += new EventHandler(dt2_Tick);
            DT2.Interval = new TimeSpan(0, 0, 2);
            DT2.Start();
            setText(incomString);
        }

        private void setText(String val)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (UnExit.instance == null)
                {
                    UnExit newun = new UnExit();
                    newun.incomingString(val, Username);
                    UnExit.instance.Show();
                }

                else
                {
                    if (UnExit.instance.IsActive == false)
                    {
                        try
                        {
                            UnExit newun = new UnExit();
                            UnExit.instance.incomingString(val, Username);
                            UnExit.instance.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageOk message = new MessageOk("تنبيه ........ ", ex.ToString(), @"/Images/information.png", @"/Images/askquestion.png", "b");
                            message.ShowDialog();
                        }
                    }
                    else
                    { 
                        UnExit.instance.incomingString(val, Username);
                    }

                }
            });
            ((Exit)this.Exitwin).filldata();
            Notification();
        }

        public void stopAlart()
        {
            sp.Write("2");
        }

        private void Warnning()
        {
            var Employee = po.Employees.FirstOrDefault(x => x.Name == Username);

            if (Employee.Position == "M")
            {
                var exisitun = po.Unauthorizeed_exit.FirstOrDefault(x => x.Information == "");
                if (exisitun != null)
                {
                    Notification();
                    var username = po.Employees.FirstOrDefault(x => x.User_id == exisitun.User_id);
                    var product = po.Products.FirstOrDefault(x => x.Pro_id == exisitun.Pro_id);
                    string text = " هناك  ";
                    string text1 = " عمليات خروج لم يتم أسناد لها تقرير ";
                    string text2 = "تأكد من نظام الكيمرات وقم بأسناد التقرير لها  ";
                    Warning messageYesNo = new Warning("Warnning ", text, Notficationcount.ToString(), text1, text2, @"/Images/error.png", @"/Images/error.png", "g", exisitun.Id);
                    messageYesNo.ShowDialog();
                }
            }
        }

        public void Notification()
        {
            int counter = po.Unauthorizeed_exit.Count(x => x.Information == "");
            var Employee = po.Employees.FirstOrDefault(x => x.Name == Username);
            if (Employee.Position == "M")
            {
                this.Dispatcher.Invoke(() =>
                    {
                        if (counter > 0)
                        {

                            txtNotification.IsVisible = true;
                            txtNotification.Label = "+" + counter.ToString();
                            Notficationcount = counter;
                        }

                        else
                            txtNotification.IsVisible = false;
                    });

            }
        }

        private void btnproducts_Click(object sender, RoutedEventArgs e)
        {
            this.griheader.Background = btnproducts.Background;
            this.txtheader.Text = "Products";
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            txtheader.Text = "Home";
            griheader.Background = btnhome.Background;          
            if (Productswin.Visibility == Visibility.Visible)
               Productswin.Visibility = Visibility.Hidden;
            else if (Exitwin.Visibility == Visibility.Visible)
               Exitwin.Visibility = Visibility.Hidden;
            else if(Userswin.Visibility == Visibility.Visible)
                 Userswin.Visibility = Visibility.Hidden;
            else if (Acountswin.Visibility == Visibility.Visible)
                Acountswin.Visibility = Visibility.Hidden;


        }

        private void btnproducts_Click_1(object sender, RoutedEventArgs e)
        {
            txtheader.Text = "Products";
            griheader.Background = btnproducts.Background;
            Productswin.Visibility = Visibility.Visible;
             if (Exitwin.Visibility == Visibility.Visible)
                Exitwin.Visibility = Visibility.Hidden;
            else if (Userswin.Visibility == Visibility.Visible)
               Userswin.Visibility = Visibility.Hidden;
            else if (Acountswin.Visibility == Visibility.Visible)
                Acountswin.Visibility = Visibility.Hidden;
        }

        private void btnUnauthorized_Click(object sender, RoutedEventArgs e)
        {
            txtheader.Text = "Exit";
            griheader.Background = btnUnauthorized.Background;
            Exitwin.Visibility = Visibility.Visible;
            if (Productswin.Visibility == Visibility.Visible)
               Productswin.Visibility = Visibility.Hidden;
           else if (Userswin.Visibility == Visibility.Visible)
               Userswin.Visibility = Visibility.Hidden;
            else if (Acountswin.Visibility == Visibility.Visible)
                Acountswin.Visibility = Visibility.Hidden;



        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            txtheader.Text = "Users";
            griheader.Background = btnUsers.Background;
           Userswin.Visibility = Visibility.Visible;
            if (Productswin.Visibility == Visibility.Visible)
                Productswin.Visibility = Visibility.Hidden;
            else if (Exitwin.Visibility == Visibility.Visible)
               Exitwin.Visibility = Visibility.Hidden;
            else if (Acountswin.Visibility == Visibility.Visible)
                Acountswin.Visibility = Visibility.Hidden;
        }

        private void btnAconts_Click(object sender, RoutedEventArgs e)
        {
            txtheader.Text = "Accounts";
            griheader.Background = btnAconts.Background;
            Acountswin.Visibility = Visibility.Visible;
            if (Productswin.Visibility == Visibility.Visible)
                Productswin.Visibility = Visibility.Hidden;
            else if (Exitwin.Visibility == Visibility.Visible)
                Exitwin.Visibility = Visibility.Hidden;
            else if (Userswin.Visibility == Visibility.Visible)
                Userswin.Visibility = Visibility.Hidden;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
           MessageYesNo messageYesNo = new MessageYesNo("تسجيل الخروج  ", "هل تود بالفعل تسجيل الخروج ", @"/Images/information.png", @"/Images/askquestion.png","o");
           messageYesNo.ShowDialog();
           if (messageYesNo.DialogResultRetern == true)
            {
                LoginWin lo = new LoginWin();
                lo.Show();
                Close();
                sp.Close();
            }           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DT1.Tick += new EventHandler(dt1_Tick);
            DT1.Interval = new TimeSpan(0, 0, 3);
            DT1.Start();
            Employee emb = po.Employees.FirstOrDefault(x => x.Name == Username);
            if (emb.Position=="S")
            {     
                btnAconts.Visibility = Visibility.Hidden;
                btnExit.Margin = btnAconts.Margin;
                ((Exit)this.Exitwin).btnDellet.Visibility = Visibility.Hidden;
                ((Exit)this.Exitwin).btnDelletall.Visibility = Visibility.Hidden;
                ((Products)this.Productswin).btnDelet.Visibility = Visibility.Hidden;
                ((Products)this.Productswin).btnedit.Visibility = Visibility.Hidden;
                ((Products)this.Productswin).btnAdd.Visibility = Visibility.Hidden;
                ((Products)this.Productswin).btnproinfo.Margin = ((Products)this.Productswin).btnedit.Margin;
                ((Products)this.Productswin).btnproinfoClose.Margin = ((Products)this.Productswin).btnedit.Margin;
                ((Products)this.Productswin).btnSearch.Margin = ((Products)this.Productswin).btnAdd.Margin;
                ((Products)this.Productswin).btnNwe.Margin = ((Products)this.Productswin).btnDelet.Margin;
                ((Products)this.Productswin).btnChooce.Visibility = Visibility.Hidden;  
                ((Users)this.Userswin).btnAdd.Visibility = Visibility.Hidden;
                ((Users)this.Userswin).btnDelet.Visibility = Visibility.Hidden;
                ((Users)this.Userswin).btnedit.Visibility = Visibility.Hidden;
                ((Users)this.Userswin).btnNwe.Visibility = Visibility.Hidden;
            }

            else if(emb.Position=="A")
            {
              btnAconts.Visibility = Visibility.Hidden;
              btnExit.Margin = btnAconts.Margin;
              ((Exit)this.Exitwin).btnDellet.Visibility = Visibility.Hidden;
              ((Exit)this.Exitwin).btnDelletall.Visibility = Visibility.Hidden;
              ((Products)this.Productswin).btnChooce.Visibility = Visibility.Hidden;

            }
        }

        public bool UserPosition()
        {
            Employee emb = po.Employees.FirstOrDefault(x => x.Name == Username);
            if (emb.Position == "M")
                return true;
            else
                return false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            
                DragMove();
            

        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
