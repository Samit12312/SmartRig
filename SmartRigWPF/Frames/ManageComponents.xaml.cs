using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Models;
using ApiClient;

namespace SmartRigWPF.Frames
{
    public partial class ManageComponents : UserControl
    {
        List<object> allComponents = new List<object>();
        bool isLoaded = false;

        public ManageComponents()
        {
            InitializeComponent();
            Loaded += ManageComponents_Loaded;
        }

        private async void ManageComponents_Loaded(object sender, RoutedEventArgs e)
        { 
            // Disable filter while loading
            ComponentTypeComboBox.IsEnabled = false;

            await LoadAllComponents();

            // Enable filter after loading
            ComponentTypeComboBox.IsEnabled = true;
            isLoaded = true;
        }

        private async Task LoadAllComponents()
        {
            try
            {
                allComponents.Clear();

                // Load CPUs
                WebClient<List<Cpu>> cpuClient = new WebClient<List<Cpu>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllCpus"
                };
                var cpus = await cpuClient.GetAsync();
                if (cpus != null) allComponents.AddRange(cpus);

                // Load GPUs
                WebClient<List<Gpu>> gpuClient = new WebClient<List<Gpu>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllGpus"
                };
                var gpus = await gpuClient.GetAsync();
                if (gpus != null) allComponents.AddRange(gpus);

                // Load RAMs
                WebClient<List<Ram>> ramClient = new WebClient<List<Ram>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllRams"
                };
                var rams = await ramClient.GetAsync();
                if (rams != null) allComponents.AddRange(rams);

                // Load Storages
                WebClient<List<Storage>> storageClient = new WebClient<List<Storage>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllStorages"
                };
                var storages = await storageClient.GetAsync();
                if (storages != null) allComponents.AddRange(storages);

                // Load Motherboards
                WebClient<List<MotherBoard>> motherboardClient = new WebClient<List<MotherBoard>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllMotherBoards"
                };
                var motherboards = await motherboardClient.GetAsync();
                if (motherboards != null) allComponents.AddRange(motherboards);

                // Load CPU Fans
                WebClient<List<CpuFan>> fanClient = new WebClient<List<CpuFan>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllCpuFans"
                };
                var fans = await fanClient.GetAsync();
                if (fans != null) allComponents.AddRange(fans);

                // Load Power Supplies
                WebClient<List<PowerSupply>> psuClient = new WebClient<List<PowerSupply>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllPowerSupplies"
                };
                var psus = await psuClient.GetAsync();
                if (psus != null) allComponents.AddRange(psus);

                // Load Operating Systems
                WebClient<List<Models.OperatingSystem>> osClient = new WebClient<List<Models.OperatingSystem>>
                {
                    Schema = "http",
                    Host = "localhost",
                    Port = 5195,
                    Path = "api/Manager/GetAllOperatingSystems"
                };
                var oses = await osClient.GetAsync();
                if (oses != null) allComponents.AddRange(oses);

                // Only set ItemsSource if listView is not null
                if (listView != null)
                {
                    listView.ItemsSource = allComponents;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading components: {ex.Message}");
            }
        }

        private void ComponentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Don't filter if not loaded yet
            if (!isLoaded || ComponentTypeComboBox.SelectedItem == null || listView == null) return;

            string selectedType = ((ComboBoxItem)ComponentTypeComboBox.SelectedItem).Content.ToString();

            if (selectedType == "All")
            {
                listView.ItemsSource = allComponents;
            }
            else if (selectedType == "CPU")
            {
                listView.ItemsSource = allComponents.OfType<Cpu>().ToList();
            }
            else if (selectedType == "GPU")
            {
                listView.ItemsSource = allComponents.OfType<Gpu>().ToList();
            }
            else if (selectedType == "RAM")
            {
                listView.ItemsSource = allComponents.OfType<Ram>().ToList();
            }
            else if (selectedType == "Storage")
            {
                listView.ItemsSource = allComponents.OfType<Storage>().ToList();
            }
            else if (selectedType == "Motherboard")
            {
                listView.ItemsSource = allComponents.OfType<MotherBoard>().ToList();
            }
            else if (selectedType == "CPU Fan")
            {
                listView.ItemsSource = allComponents.OfType<CpuFan>().ToList();
            }
            else if (selectedType == "Power Supply")
            {
                listView.ItemsSource = allComponents.OfType<PowerSupply>().ToList();
            }
            else if (selectedType == "Operating System")
            {
                listView.ItemsSource = allComponents.OfType<Models.OperatingSystem>().ToList();
            }
        }
    }
}