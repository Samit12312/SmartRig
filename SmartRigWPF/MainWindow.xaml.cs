using System.Windows;
using System.Windows.Navigation;
using SmartRigWPF.Frames;

namespace SmartRigWPF
{
    public partial class MainWindow : Window
    {
        bool IsAdmin = false;
        string currentUserName = "";

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
                HelloText.Text = "";
                HelloText.Visibility = Visibility.Collapsed;

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
                HelloText.Text = "Hello, " + currentUserName;
                HelloText.Visibility = Visibility.Visible;

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

        private void ClearPages()
        {
            startPage = null;
            loginPage = null;
            manageUsers = null;
            manageComputers = null;
            manageComponents = null;
            manageOrders = null;
            manageReports = null;
        }

        public void ViewLoginPage()
        {
            loginPage = new LoginPage();
            ContentFrame.Content = loginPage;
        }

        public void ViewStartPage(bool isLogin)
        {
            IsAdmin = isLogin;

            startPage = new StartPage();
            ContentFrame.Content = startPage;

            UpdateMain();
        }

        public void ViewManageUsers()
        {
            manageUsers = new ManageUsers();
            ContentFrame.Content = manageUsers;
        }

        public void ViewManageComputers()
        {
            manageComputers = new ManageComputers();
            ContentFrame.Content = manageComputers;
        }

        public void ViewManageComponents()
        {
            manageComponents = new ManageComponents();
            ContentFrame.Content = manageComponents;
        }

        public void ViewManageOrders()
        {
            manageOrders = new ManageOrders();
            ContentFrame.Content = manageOrders;
        }

        public void ViewManageReports()
        {
            manageReports = new ManageReports();
            ContentFrame.Content = manageReports;
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
                currentUserName = "";

                ClearPages();
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ViewManageUsers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewManageReports();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        public void LoginSuccess(string userName)
        {
            IsAdmin = true;
            currentUserName = userName;

            ClearPages();
            UpdateMain();
            ViewStartPage(true);
        }
    }
}