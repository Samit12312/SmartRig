using ApiClient;
using Models;
using Models.ViewModels;
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

namespace SmartRigWPF.Frames
{
    public partial class AddUser : Window
    {
        User selectedUser;

        public AddUser(User user)
        {
            InitializeComponent();
            this.selectedUser = user;
            GetCities();
        }

        private async Task GetCities()
        {
            WebClient<List<Cities>> client = new WebClient<List<Cities>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetAllCities";
            List<Cities> cities = await client.GetAsync();

            if (cities != null)
            {
                CityBox.ItemsSource = cities;
            }

            UserNameBox.Text = selectedUser.UserName;
            UserEmailBox.Text = selectedUser.UserEmail;
            UserPhoneBox.Text = selectedUser.UserPhoneNumber;
            UserAddressBox.Text = selectedUser.UserAddress;
            CityBox.SelectedValue = selectedUser.CityId;
            ManagerBox.IsChecked = selectedUser.Manager;

            this.Title = "Edit User";
            AddBtn.Content = "Update User";
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            WebClient<EditUserViewModel> client = new WebClient<EditUserViewModel>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/EditUser";

            EditUserViewModel data = new EditUserViewModel();
            data.UserId = selectedUser.UserId;
            data.UserName = UserNameBox.Text;
            data.UserEmail = UserEmailBox.Text;
            data.UserPhoneNumber = UserPhoneBox.Text;
            data.UserAddress = UserAddressBox.Text;
            data.CityId = (int)CityBox.SelectedValue;
            data.Manager = ManagerBox.IsChecked == true;

            bool ok = await client.PostAsync(data);

            if (ok) { this.DialogResult = true; MessageBox.Show("User Updated"); this.Close(); }
            else { MessageBox.Show("Failed to update user", "", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}