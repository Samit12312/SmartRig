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
using Models.ViewModels;
using ApiClient;

namespace SmartRigWPF.Frames
{
    public partial class ManageComputers : UserControl
    {
        List<ComputersViewModel> computers = new List<ComputersViewModel>();

        public ManageComputers()
        {
            InitializeComponent();
            GetComputers();
        }

        private async Task GetComputers()
        {
            WebClient<List<ComputersViewModel>> client = new WebClient<List<ComputersViewModel>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetAllComputers";
            this.computers = await client.GetAsync();
            listView.ItemsSource = this.computers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddComputer addComputer = new AddComputer();
            addComputer.ShowDialog();
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("Select a computer first");
                return;
            }
            ComputersViewModel selected = (ComputersViewModel)listView.SelectedItem;
            AddComputer win = new AddComputer(selected.computer);
            win.ShowDialog();
            await GetComputers();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("Please select a computer to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ComputersViewModel selected = (ComputersViewModel)listView.SelectedItem;
            MessageBoxResult confirmation = MessageBox.Show(
                $"Are you sure you want to delete '{selected.computer.ComputerName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (confirmation == MessageBoxResult.Yes)
            {
                try
                {
                    WebClient<bool> client = new WebClient<bool>();
                    client.Schema = "http";
                    client.Host = "localhost";
                    client.Port = 5195;
                    client.Path = "api/Manager/RemoveComputer";
                    client.AddParameter("computerId", selected.computer.ComputerId.ToString());
                    bool success = await client.GetAsync();
                    if (success)
                    {
                        MessageBox.Show("Computer deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        await GetComputers();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}