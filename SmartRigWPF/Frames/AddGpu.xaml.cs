using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddGpu : Window
    {
        bool isEdit = false;
        Gpu selectedGpu = null;

        public AddGpu()
        {
            InitializeComponent();
            Loaded += AddGpu_Loaded;
        }

        public AddGpu(Gpu gpu)
        {
            InitializeComponent();
            selectedGpu = gpu;
            isEdit = true;
            Loaded += AddGpu_Loaded;
        }

        private async void AddGpu_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();

            if (isEdit && selectedGpu != null)
            {
                GpuNameBox.Text = selectedGpu.GpuName;
                SizeBox.Text = selectedGpu.GpuSize.ToString();
                SpeedBox.Text = selectedGpu.GpuSpeed.ToString();
                PriceBox.Text = selectedGpu.GpuPrice.ToString();
                CompanyBox.SelectedValue = selectedGpu.GpuCompanyId;

                Title = "Edit Gpu";
                AddBtn.Content = "Update GPU";
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
            Gpu gpu = new Gpu();
            gpu.GpuName = GpuNameBox.Text;
            gpu.GpuSize = SizeBox.Text;
            gpu.GpuSpeed = SpeedBox.Text;
            gpu.GpuPrice = int.Parse(PriceBox.Text);
            gpu.GpuCompanyId = (int)CompanyBox.SelectedValue;

            WebClient<Gpu> client = new WebClient<Gpu>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                gpu.GpuId = selectedGpu.GpuId;
                client.Path = "api/Manager/EditGpu";
                ok = await client.PostAsync(gpu);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("GPU Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update GPU");
                }
            }
            else
            {
                client.Path = "api/Manager/AddGpu";
                ok = await client.PostAsync(gpu);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("GPU Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add GPU");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}