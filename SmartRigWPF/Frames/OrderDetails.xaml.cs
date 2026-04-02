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
using System.Windows.Shapes;
using Models;
using ApiClient;

namespace SmartRigWPF.Frames
{
    public partial class OrderDetails : Window
    {
        Cart selectedCart;

        public OrderDetails(Cart cart)
        {
            InitializeComponent();
            this.selectedCart = cart;
            LoadOrderDetails();
        }

        private async Task LoadOrderDetails()
        {
            OrderIdText.Text = $"Order #: {selectedCart.CartId}";
            UserIdText.Text = $"User ID: {selectedCart.UserId}";
            DateText.Text = $"Date: {selectedCart.Date}";
            IsPayedBox.IsChecked = selectedCart.IsPayed;

            WebClient<List<CartComputer>> client = new WebClient<List<CartComputer>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetOrderById";
            client.AddParameter("userId", selectedCart.UserId.ToString());
            List<CartComputer> computers = await client.GetAsync();
            ComputersListView.ItemsSource = computers;
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            WebClient<bool> client = new WebClient<bool>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/ChangeCartStatus";
            client.AddParameter("cartId", selectedCart.CartId.ToString());
            client.AddParameter("isPayed", (IsPayedBox.IsChecked == true).ToString());
            bool ok = await client.GetAsync();

            if (ok) { this.DialogResult = true; MessageBox.Show("Order updated!"); this.Close(); }
            else { MessageBox.Show("Failed to update order", "", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}