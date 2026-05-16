using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddCpu : Window
    {
        bool isEdit = false;
        Cpu selectedCpu = null;

        public AddCpu()
        {
            InitializeComponent();
            Loaded += AddCpu_Loaded;
        }

        public AddCpu(Cpu cpu)
        {
            InitializeComponent();
            selectedCpu = cpu;
            isEdit = true;
            Loaded += AddCpu_Loaded;
        }

        private async void AddCpu_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();

            if (isEdit && selectedCpu != null)
            {
                CpuNameBox.Text = selectedCpu.CpuName;
                PriceBox.Text = selectedCpu.CpuPrice.ToString();
                CompanyBox.SelectedValue = selectedCpu.CpuCompanyId;

                Title = "Edit Cpu";
                AddBtn.Content = "Update CPU";
            }
        }

        private async Task LoadCompanies()
        {
            WebClient<List<Company>> client = new WebClient<List<Company>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetAllCompanies";

            List<Company> companies = await client.GetAsync();

            if (companies != null)
            {
                CompanyBox.ItemsSource = companies;
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Cpu cpu = new Cpu();
            cpu.CpuName = CpuNameBox.Text;
            bool ok = int.TryParse(PriceBox.Text, out int price);
            if (ok)
                cpu.CpuPrice = price;
            else
                cpu.CpuPrice = -1;
            cpu.CpuCompanyId = (int)CompanyBox.SelectedValue;
            cpu.Validate(); 
            if(cpu.HasErrors)
            {
                Dictionary<string, List<string>> errors = cpu.AllErrors();
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in errors) {
                    errorMessage.AppendLine($"{error.Key}:/n ");
                    foreach (var errorDetail in error.Value)
                    {
                        errorMessage.AppendLine($" - {errorDetail}\n");
                    }
                }
                MessageBox.Show(errorMessage.ToString(),"Correct next errors",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }   
            WebClient<Cpu> client = new WebClient<Cpu>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

             ok = false;

            if (isEdit)
            {
                cpu.CpuId = selectedCpu.CpuId;
                client.Path = "api/Manager/EditCpu";
                ok = await client.PostAsync(cpu);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("CPU Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update CPU");
                }
            }
            else
            {
                client.Path = "api/Manager/AddCpu";
                ok = await client.PostAsync(cpu);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("CPU Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add CPU");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}