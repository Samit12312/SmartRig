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
            if (selectedUser == null)
            {
                MessageBox.Show("You must choose a user first", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EditUserViewModel data = new EditUserViewModel();

            data.UserId = selectedUser.UserId;
            data.UserName = UserNameBox.Text;
            data.UserEmail = UserEmailBox.Text;
            data.UserPhoneNumber = UserPhoneBox.Text;
            data.UserAddress = UserAddressBox.Text;
            data.CityId = CityBox.SelectedValue == null ? 0 : (int)CityBox.SelectedValue;
            data.Manager = ManagerBox.IsChecked == true;
            data.UserPassword = UserPasswordBox.Password;

            data.Validate();

            if (data.HasErrors)
            {
                string message = "";

                Dictionary<string, List<string>> errors = data.AllErrors();

                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorMessage in error.Value)
                    {
                        message += errorMessage + "\n";
                    }
                }

                MessageBox.Show(message, "User is not valid", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}