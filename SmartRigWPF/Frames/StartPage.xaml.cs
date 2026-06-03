using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartRigWPF.Frames
{
    public partial class StartPage : UserControl
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ManageUsers());
        }

        private void ComputersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ManageComputers());
        }

        private void ComponentsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ManageComponents());
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ManageOrders());
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ManageReports());
        }

        private void NavigateToPage(UserControl page)
        {
            DependencyObject parent = this;

            while (parent != null)
            {
                if (parent is Frame frame)
                {
                    frame.Navigate(page);
                    return;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }
        }
    }
}