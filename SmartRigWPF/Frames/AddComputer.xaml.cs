using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using ApiClient;
using Microsoft.Win32;
using Models;
using Models.ViewModels;
using System.IO;
namespace SmartRigWPF.Frames
{
    /// <summary>
    /// Interaction logic for AddComputer.xaml
    /// </summary>
    public partial class AddComputer : Window
    {
        string imgPath;
        public AddComputer()
        {
            InitializeComponent();
            GetNewComputerViewModel();
        }
        private async Task GetNewComputerViewModel()
        {
            WebClient<NewComputerViewModel> client = new WebClient<NewComputerViewModel>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetNewComputerViewModel";
            NewComputerViewModel viewModel = await client.GetAsync();
            if (viewModel != null)
            {
                CompanyBox.ItemsSource = viewModel.Companies;
                TypeBox.ItemsSource = viewModel.Types;
                OSBox.ItemsSource = viewModel.OS;
                CpuBox.ItemsSource = viewModel.Cpus;
                GpuBox.ItemsSource = viewModel.Gpus;
                RamBox.ItemsSource = viewModel.Rams;
                StorageBox.ItemsSource = viewModel.Storages;
                MotherboardBox.ItemsSource = viewModel.Motherboards;
                CaseBox.ItemsSource = viewModel.Cases;
                CpuFanBox.ItemsSource = viewModel.Fans;
                PowerSupplyBox.ItemsSource = viewModel.PowerSupplies;
                if (viewModel.Computer == null) {viewModel.Computer = new Computer(); }
            }

            this.DataContext = viewModel;
        }
        private void UploadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            bool? ok = ofd.ShowDialog();
            if (ok == true)
            {
                Uri uri = new Uri(ofd.FileName);
                this.image.Source = new BitmapImage(uri);
                this.imgPath = ofd.FileName;
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewComputerViewModel ncvm = new NewComputerViewModel();
            Computer computer = new Computer();
            computer.ComputerName = ComputerNameBox.Text;
            computer.Price = int.Parse(PriceBox.Text);
            computer.CompanyId = (int)CompanyBox.SelectedValue;
            computer.ComputerTypeId = (int)TypeBox.SelectedValue;
            computer.OperatingSystemId = (int)OSBox.SelectedValue;
            computer.CpuId = (int)CpuBox.SelectedValue;
            computer.GpuId = (int)GpuBox.SelectedValue;
            computer.RamId = (int)RamBox.SelectedValue;
            computer.StorageId = (int)StorageBox.SelectedValue;
            computer.MotherBoardId = (int)MotherboardBox.SelectedValue;
            computer.CaseId = (int)CaseBox.SelectedValue;
            computer.CpuFanId = (int)CpuFanBox.SelectedValue;
            computer.PowerSupplyId = (int)PowerSupplyBox.SelectedValue;
            computer.ComputerPicture = System.IO.Path.GetExtension(this.imgPath);
            Stream stream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            WebClient<Computer> client = new WebClient<Computer>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/AddComputer";
            bool ok = await client.PostAsync(computer, stream);
            if (ok) { this.DialogResult = true; MessageBox.Show("Computer Added"); this.Close(); }
            else {  MessageBox.Show("Failed to add computer","", MessageBoxButton.OK , MessageBoxImage.Error); }
        }
    }
}
