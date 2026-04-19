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

            if (selectedUser != null)
            {
                UserNameBox.Text = selectedUser.UserName;
                UserEmailBox.Text = selectedUser.UserEmail;
                UserPhoneBox.Text = selectedUser.UserPhoneNumber;
                UserAddressBox.Text = selectedUser.UserAddress;
                ManagerBox.IsChecked = selectedUser.Manager;
            }

            LoadCities();
        }




        private async void LoadCities()
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
                CityBox.SelectedValue = selectedUser.CityId;
            }
            else
            {
                MessageBox.Show("Failed to load cities");
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {


            EditUserViewModel data = new EditUserViewModel();
            data.user.UserId = selectedUser.UserId;
            data.user.UserName = UserNameBox.Text;
            data.user.UserEmail = UserEmailBox.Text;
            data.user.UserPhoneNumber = UserPhoneBox.Text;
            data.user.UserAddress = UserAddressBox.Text;
            data.user.CityId = CityBox.SelectedValue == null ? 0 : (int)CityBox.SelectedValue;
            data.user.Manager = ManagerBox.IsChecked == true;
            if (UserPasswordBox.Password != "")
            {
                data.user.UserPassword = UserPasswordBox.Password;
            }
            data.user.Validate();
            bool isValid = data.user.IsValid;
             if (isValid)
            {
                WebClient<EditUserViewModel> client = new WebClient<EditUserViewModel>();
                client.Schema = "http";
                client.Host = "localhost";
                client.Port = 5195;
                client.Path = "api/Manager/EditUser";

                bool ok = await client.PostAsync(data);

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("User Updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update user", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to update user", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}