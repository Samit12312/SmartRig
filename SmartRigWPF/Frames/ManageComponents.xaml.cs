using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Models;
using ApiClient;

namespace SmartRigWPF.Frames
{
    public partial class ManageComponents : UserControl
    {
        public ManageComponents()
        {
            InitializeComponent();
            LoadComponents();
        }

        private async void LoadComponents()
        {
            await LoadCpus();
            await LoadGpus();
            await LoadRams();
            await LoadStorages();
            await LoadMotherBoards();
            await LoadCpuFans();
            await LoadPowerSupplies();
            await LoadOperatingSystems();
        }

        private async Task LoadCpus()
        {
            WebClient<List<Cpu>> client = new WebClient<List<Cpu>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllCpus"
            };
            cpuListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadGpus()
        {
            WebClient<List<Gpu>> client = new WebClient<List<Gpu>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllGpus"
            };
            gpuListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadRams()
        {
            WebClient<List<Ram>> client = new WebClient<List<Ram>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllRams"
            };
            ramListView.ItemsSource = await client.GetAsync();
        }
        private async Task LoadStorages()
        {
            WebClient<List<Storage>> client = new WebClient<List<Storage>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllStorages"
            };
            storageListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadMotherBoards()
        {
            WebClient<List<MotherBoard>> client = new WebClient<List<MotherBoard>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllMotherBoards"
            };
            motherBoardListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadCpuFans()
        {
            WebClient<List<CpuFan>> client = new WebClient<List<CpuFan>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllCpuFans"
            };
            cpuFanListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadPowerSupplies()
        {
            WebClient<List<PowerSupply>> client = new WebClient<List<PowerSupply>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllPowerSupplies"
            };
            powerSupplyListView.ItemsSource = await client.GetAsync();
        }

        private async Task LoadOperatingSystems()
        {
            WebClient<List<Models.OperatingSystem>> client = new WebClient<List<Models.OperatingSystem>>()
            {
                Schema = "http",
                Host = "localhost",
                Port = 5195,
                Path = "api/Manager/GetAllOperatingSystems"
            };
            osListView.ItemsSource = await client.GetAsync();
        }

        // Button handlers
        private void AddStorage_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add Storage");
        private void EditStorage_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit Storage");
        private void DeleteStorage_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete Storage");

        private void AddMotherBoard_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add MotherBoard");
        private void EditMotherBoard_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit MotherBoard");
        private void DeleteMotherBoard_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete MotherBoard");

        private void AddCpuFan_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add CPU Fan");
        private void EditCpuFan_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit CPU Fan");
        private void DeleteCpuFan_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete CPU Fan");

        private void AddPowerSupply_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add PSU");
        private void EditPowerSupply_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit PSU");
        private void DeletePowerSupply_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete PSU");

        private void AddOs_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add OS");
        private void EditOs_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit OS");
        private void DeleteOs_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete OS");
        // Repeat similar methods for Storage, Motherboard, CPU Fan, Power Supply, OS

        private void AddCpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add CPU");
        private void EditCpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit CPU");
        private void DeleteCpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete CPU");

        private void AddGpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add GPU");
        private void EditGpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit GPU");
        private void DeleteGpu_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete GPU");

        private void AddRam_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add RAM");
        private void EditRam_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Edit RAM");
        private void DeleteRam_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Delete RAM");
    }
}