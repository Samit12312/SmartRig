using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Models;
using Models.ViewModels;
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
            ComponentTypeComboBox.IsEnabled = false;

            await LoadAllComponents();

            ComponentTypeComboBox.IsEnabled = true;
            isLoaded = true;
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select filter first");
                return;
            }

            string selectedType = ((ComboBoxItem)ComponentTypeComboBox.SelectedItem).Content.ToString();
            bool? result = false;

            if (selectedType == "CPU")
            {
                AddCpu window = new AddCpu();
                result = window.ShowDialog();
            }
            else if (selectedType == "CPU Fan")
            {
                AddCpuFan window = new AddCpuFan();
                result = window.ShowDialog();
            }
            else if (selectedType == "Case")
            {
                AddCase window = new AddCase();
                result = window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Add is ready now for CPU, CPU Fan and Case only");
                return;
            }

            if (result == true)
            {
                await LoadAllComponents();
                ComponentTypeComboBox_SelectionChanged(null, null);
            }
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("Select something first");
                return;
            }

            bool? result = false;

            if (listView.SelectedItem is CpuManageViewModel cpuVm)
            {
                AddCpu window = new AddCpu(cpuVm.cpu);
                result = window.ShowDialog();
            }
            else if (listView.SelectedItem is CpuFanManageViewModel cpuFanVm)
            {
                AddCpuFan window = new AddCpuFan(cpuFanVm.cpuFan);
                result = window.ShowDialog();
            }
            else if (listView.SelectedItem is CaseManageViewModel caseVm)
            {
                AddCase window = new AddCase(caseVm.computerCase);
                result = window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Edit is ready now for CPU, CPU Fan and Case only");
                return;
            }

            if (result == true)
            {
                await LoadAllComponents();
                ComponentTypeComboBox_SelectionChanged(null, null);
            }
        }

        private async Task LoadAllComponents()
        {
            try
            {
                allComponents.Clear();

                WebClient<ManageComponentsViewModel> client = new WebClient<ManageComponentsViewModel>();
                client.Schema = "http";
                client.Host = "localhost";
                client.Port = 5195;
                client.Path = "api/Manager/GetAllManageComponents";

                ManageComponentsViewModel vm = await client.GetAsync();

                if (vm != null)
                {
                    if (vm.cpus != null) allComponents.AddRange(vm.cpus);
                    if (vm.gpus != null) allComponents.AddRange(vm.gpus);
                    if (vm.rams != null) allComponents.AddRange(vm.rams);
                    if (vm.storages != null) allComponents.AddRange(vm.storages);
                    if (vm.motherBoards != null) allComponents.AddRange(vm.motherBoards);
                    if (vm.cpuFans != null) allComponents.AddRange(vm.cpuFans);
                    if (vm.powerSupplies != null) allComponents.AddRange(vm.powerSupplies);
                    if (vm.cases != null) allComponents.AddRange(vm.cases);
                    if (vm.operatingSystems != null) allComponents.AddRange(vm.operatingSystems);
                }

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
            if (!isLoaded || ComponentTypeComboBox.SelectedItem == null || listView == null) return;

            string selectedType = ((ComboBoxItem)ComponentTypeComboBox.SelectedItem).Content.ToString();

            if (selectedType == "All")
            {
                listView.ItemsSource = allComponents;
            }
            else if (selectedType == "CPU")
            {
                listView.ItemsSource = allComponents.OfType<CpuManageViewModel>().ToList();
            }
            else if (selectedType == "GPU")
            {
                listView.ItemsSource = allComponents.OfType<GpuManageViewModel>().ToList();
            }
            else if (selectedType == "RAM")
            {
                listView.ItemsSource = allComponents.OfType<RamManageViewModel>().ToList();
            }
            else if (selectedType == "Storage")
            {
                listView.ItemsSource = allComponents.OfType<StorageManageViewModel>().ToList();
            }
            else if (selectedType == "Motherboard")
            {
                listView.ItemsSource = allComponents.OfType<MotherBoardManageViewModel>().ToList();
            }
            else if (selectedType == "Case")
            {
                listView.ItemsSource = allComponents.OfType<CaseManageViewModel>().ToList();
            }
            else if (selectedType == "CPU Fan")
            {
                listView.ItemsSource = allComponents.OfType<CpuFanManageViewModel>().ToList();
            }
            else if (selectedType == "Power Supply")
            {
                listView.ItemsSource = allComponents.OfType<PowerSupplyManageViewModel>().ToList();
            }
            else if (selectedType == "Operating System")
            {
                listView.ItemsSource = allComponents.OfType<OperatingSystemManageViewModel>().ToList();
            }
        }
    }
}