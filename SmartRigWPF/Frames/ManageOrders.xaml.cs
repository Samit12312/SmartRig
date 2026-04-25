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
using Models;
using ApiClient;

namespace SmartRigWPF.Frames
{
    public partial class ManageOrders : UserControl
    {
        List<Cart> orders = new List<Cart>();

        public ManageOrders()
        {
            InitializeComponent();
            GetOrders();
        }

        private async Task GetOrders()
        {
            WebClient<List<Cart>> client = new WebClient<List<Cart>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetAllOrders";
            this.orders = await client.GetAsync();
            listView.ItemsSource = this.orders;
        }

        private async void MarkAsPaidBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("select an order first");
                return;
            }
            Cart selectedOrder = (Cart)listView.SelectedItem;

            WebClient<bool> client = new WebClient<bool>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/ChangeCartStatus";
            client.AddParameter("cartId", selectedOrder.CartId.ToString());
            client.AddParameter("isPayed", "true");
            bool ok = await client.GetAsync();

            if (ok) { MessageBox.Show("order marked as paid!"); await GetOrders(); }
            else { MessageBox.Show("failed to update order", "", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private async void DeleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("select an order first");
                return;
            }
            Cart selectedOrder = (Cart)listView.SelectedItem;
            MessageBoxResult confirmation = MessageBox.Show(
                $"are you sure you want to delete order #{selectedOrder.CartId}?",
                "confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (confirmation == MessageBoxResult.Yes)
            {
                WebClient<bool> client = new WebClient<bool>();
                client.Schema = "http";
                client.Host = "localhost";
                client.Port = 5195;
                client.Path = "api/Manager/DeleteOrder";
                client.AddParameter("cartId", selectedOrder.CartId.ToString());
                bool ok = await client.GetAsync();

                if (ok) { MessageBox.Show("order deleted!"); await GetOrders(); }
                else { MessageBox.Show("failed to delete order", "", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private async void ViewDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("select an order first");
                return;
            }
            Cart selectedOrder = (Cart)listView.SelectedItem;
            OrderDetails win = new OrderDetails(selectedOrder);
            win.ShowDialog();
            await GetOrders();
        }
    }
}