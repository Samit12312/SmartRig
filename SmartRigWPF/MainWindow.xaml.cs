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
    public partial class MainWindow : Window
    {
        StartPage startPage;
        LoginPage loginPage;
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
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
        public void ViewStartPage()
        {
            if(this.startPage == null)
                this.startPage = new StartPage();
            this.ContentFrame.Content = this.startPage;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }
    }
}