using System.Windows;
using System.Windows.Controls;

namespace SmartRigWPF.Frames
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private MainWindow GetMainWindow()
        {
            return Window.GetWindow(this) as MainWindow;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = GetMainWindow();

            if (mainWindow != null)
            {
                mainWindow.ViewManageUsers();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = GetMainWindow();

            if (mainWindow != null)
            {
                mainWindow.ViewManageComputers();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = GetMainWindow();

            if (mainWindow != null)
            {
                mainWindow.ViewManageComponents();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = GetMainWindow();

            if (mainWindow != null)
            {
                mainWindow.ViewManageOrders();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = GetMainWindow();

            if (mainWindow != null)
            {
                mainWindow.ViewManageReports();
            }
        }
    }
}