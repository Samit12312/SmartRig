using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartRigWPF.Frames;

namespace SmartRigWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool IsAdmin = false;
        StartPage startPage;
        LoginPage loginPage;
        ManageUsers manageUsers;
        ManageComputers manageComputers;
        ManageComponents manageComponents;
        ManageOrders manageOrders;
        ManageReports manageReports;
        public MainWindow()
        {
            InitializeComponent();

            IsAdmin = false;
            UpdateMain();

            ViewLoginPage();
        }
        private void UpdateMain()
        {
            if (IsAdmin == false)
            {
                LoginButton.Content = "Login";

                HomeButton.Visibility = Visibility.Collapsed;

                SideBarBorder.Visibility = Visibility.Collapsed;
                SideBarColumn.Width = new GridLength(0);

                DashBoard.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
                ComputersButton.Visibility = Visibility.Collapsed;
                ComponentsButton.Visibility = Visibility.Collapsed;
                OrdersButton.Visibility = Visibility.Collapsed;
                ReportsButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoginButton.Content = "Logout";

                HomeButton.Visibility = Visibility.Visible;

                SideBarColumn.Width = new GridLength(220);
                SideBarBorder.Visibility = Visibility.Visible;

                DashBoard.Visibility = Visibility.Visible;
                UsersButton.Visibility = Visibility.Visible;
                ComputersButton.Visibility = Visibility.Visible;
                ComponentsButton.Visibility = Visibility.Visible;
                OrdersButton.Visibility = Visibility.Visible;
                ReportsButton.Visibility = Visibility.Visible;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

        }
        public void ViewLoginPage()
        {
            if (this.loginPage == null)
                this.loginPage = new LoginPage();

            this.ContentFrame.Content = this.loginPage;
        }

        public void ViewStartPage(bool isLogin)
        {
            this.IsAdmin = isLogin;

            if (this.startPage == null)
                this.startPage = new StartPage();

            this.ContentFrame.Content = this.startPage;

            UpdateMain();
        }

        public void ViewManageUsers()
        {
            if (this.manageUsers == null)
                this.manageUsers = new ManageUsers();
            this.ContentFrame.Content = this.manageUsers;
        }

        public void ViewManageComputers()
        {
            if (this.manageComputers == null)
                this.manageComputers = new ManageComputers();
            this.ContentFrame.Content = this.manageComputers;
        }

        public void ViewManageComponents()
        {
            if (this.manageComponents == null)
                this.manageComponents = new ManageComponents();
            this.ContentFrame.Content = this.manageComponents;
        }

        public void ViewManageOrders()
        {
            if (this.manageOrders == null)
                this.manageOrders = new ManageOrders();
            this.ContentFrame.Content = this.manageOrders;
        }

        public void ViewManageReports()
        {
            if (this.manageReports == null)
                this.manageReports = new ManageReports();
            this.ContentFrame.Content = this.manageReports;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin == false)
            {
                ViewLoginPage();
            }
            else
            {
                IsAdmin = false;
                UpdateMain();
                ViewLoginPage();
            }
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage(IsAdmin);
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewManageOrders();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ViewManageComponents();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ViewManageComputers();
        }
        public void LoginSuccess()
        {
            IsAdmin = true;
            LoginButton.Content = "Logout";
            UpdateMain();
            ViewStartPage(true);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ViewManageUsers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewManageReports();
        }
    }
}