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
    public partial class ManageUsers : UserControl
    {
        List<User> users = new List<User>();

        public ManageUsers()
        {
            InitializeComponent();
            GetUsers();
        }

        private async Task GetUsers()
        {
            WebClient<List<User>> client = new WebClient<List<User>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetAllUsers";
            this.users = await client.GetAsync();
            listView.ItemsSource = this.users;
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("select a user first");
                return;
            }
            User selectedUser = (User)listView.SelectedItem;
            AddUser win = new AddUser(selectedUser);
            win.ShowDialog();
            await GetUsers();
        }
    }
}