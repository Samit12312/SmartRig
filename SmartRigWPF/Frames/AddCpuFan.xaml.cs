using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;
using Models.ViewModels;

namespace SmartRigWPF.Frames
{
    public partial class AddCpuFan : Window
    {
        bool isEdit;
        CpuFan selectedCpuFan;

        public AddCpuFan()
        {
            InitializeComponent();
            GetNewCpuFanViewModel();
        }

        public AddCpuFan(CpuFan cpuFan)
        {
            InitializeComponent();
            this.selectedCpuFan = cpuFan;
            isEdit = true;
            GetNewCpuFanViewModel();
        }

        private async Task GetNewCpuFanViewModel()
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

                if (isEdit && selectedCpuFan != null)
                {
                    CpuFanNameBox.Text = selectedCpuFan.CpuFanName;
                    PriceBox.Text = selectedCpuFan.CpuFanPrice.ToString();
                    CompanyBox.SelectedValue = selectedCpuFan.CpuFanCompanyId;

                    this.Title = "Edit Cpu Fan";
                    AddBtn.Content = "Update CPU Fan";
                }
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            CpuFan cpuFan = new CpuFan();
            cpuFan.CpuFanName = CpuFanNameBox.Text;
            bool ok = int.TryParse(PriceBox.Text, out int price);
            if (ok)
                cpuFan.CpuFanPrice = price;
            else
                cpuFan.CpuFanPrice = -1;
            cpuFan.CpuFanCompanyId = (int)CompanyBox.SelectedValue;

            cpuFan.Validate();
            if (cpuFan.HasErrors)
            {
                Dictionary<string, List<string>> errors = cpuFan.AllErrors();
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in errors)
                {
                    errorMessage.AppendLine($"{error.Key}:/n ");
                    foreach (var errorDetail in error.Value)
                    {
                        errorMessage.AppendLine($" - {errorDetail}\n");
                    }
                }
                MessageBox.Show(errorMessage.ToString(), "Correct next errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WebClient<CpuFan> client = new WebClient<CpuFan>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            ok = false;

            if (isEdit)
            {
                cpuFan.CpuFanId = selectedCpuFan.CpuFanId;
                client.Path = "api/Manager/EditCpuFan";
                ok = await client.PostAsync(cpuFan);

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("CPU Fan Updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update CPU Fan");
                }
            }
            else
            {
                client.Path = "api/Manager/AddCpuFan";
                ok = await client.PostAsync(cpuFan);

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("CPU Fan Added");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add CPU Fan");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}