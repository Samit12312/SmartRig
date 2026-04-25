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
    public partial class AddComputer : Window
    {
        string imgPath;
        bool isEdit;
        Computer selectedComputer;

        public AddComputer()
        {
            InitializeComponent();
            GetNewComputerViewModel();
        }

        public AddComputer(Computer computer)
        {
            InitializeComponent();
            this.selectedComputer = computer;
            isEdit = true;
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

                if (isEdit && selectedComputer != null)
                {
                    viewModel.Computer = selectedComputer;

                    this.Title = "Edit Computer";
                    AddBtn.Content = "Update Computer";

                    if (!string.IsNullOrEmpty(selectedComputer.ComputerPicture))
                    {
                        Uri uri = new Uri("http://localhost:5195/Images/Computers/" + selectedComputer.ComputerPicture);
                        this.image.Source = new BitmapImage(uri);
                    }
                }
                else
                {
                    viewModel.Computer = new Computer();
                }

                this.DataContext = viewModel;
            }
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
            NewComputerViewModel viewModel = (NewComputerViewModel)this.DataContext;
            Computer computer = viewModel.Computer;
            int price = 0;

            try
            {
                price = Convert.ToInt32(PriceBox.Text);
            }
            catch
            {
                MessageBox.Show("Please only input number in Price");
                return;
            }

            computer.Price = price;
            computer.CompanyId = CompanyBox.SelectedValue == null ? 0 : (int)CompanyBox.SelectedValue;
            computer.ComputerTypeId = TypeBox.SelectedValue == null ? 0 : (int)TypeBox.SelectedValue;
            computer.OperatingSystemId = OSBox.SelectedValue == null ? 0 : (int)OSBox.SelectedValue;
            computer.CpuId = CpuBox.SelectedValue == null ? 0 : (int)CpuBox.SelectedValue;
            computer.GpuId = GpuBox.SelectedValue == null ? 0 : (int)GpuBox.SelectedValue;
            computer.RamId = RamBox.SelectedValue == null ? 0 : (int)RamBox.SelectedValue;
            computer.StorageId = StorageBox.SelectedValue == null ? 0 : (int)StorageBox.SelectedValue;
            computer.MotherBoardId = MotherboardBox.SelectedValue == null ? 0 : (int)MotherboardBox.SelectedValue;
            computer.CaseId = CaseBox.SelectedValue == null ? 0 : (int)CaseBox.SelectedValue;
            computer.CpuFanId = CpuFanBox.SelectedValue == null ? 0 : (int)CpuFanBox.SelectedValue;
            computer.PowerSupplyId = PowerSupplyBox.SelectedValue == null ? 0 : (int)PowerSupplyBox.SelectedValue;


            if (isEdit)
            {
                computer.ComputerId = selectedComputer.ComputerId;

                if (string.IsNullOrEmpty(imgPath))
                {
                    computer.ComputerPicture = selectedComputer.ComputerPicture;
                }
                else
                {
                    computer.ComputerPicture = System.IO.Path.GetExtension(imgPath);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(imgPath))
                {
                    MessageText.Text = "You must choose image";
                    return;
                }

                computer.ComputerPicture = System.IO.Path.GetExtension(imgPath);
            }

            computer.Validate();

            if (!computer.IsValid)
            {
                MessageText.Text = "Computer is not valid";
                return;
            }

            MessageText.Text = "";

            WebClient<Computer> client = new WebClient<Computer>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                client.Path = "api/Manager/EditComputer";

                if (!string.IsNullOrEmpty(imgPath))
                {
                    using (Stream stream = new FileStream(imgPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        ok = await client.PostAsync(computer, stream);
                    }
                }
                else
                {
                    ok = await client.PostAsync(computer);
                }

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("Computer Updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update computer", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                using (Stream stream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    client.Path = "api/Manager/AddComputer";
                    ok = await client.PostAsync(computer, stream);
                }

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("Computer Added");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add computer", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}