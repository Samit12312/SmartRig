using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddOperatingSystem : Window
    {
        bool isEdit = false;
        Models.OperatingSystem selectedOperatingSystem = null;

        public AddOperatingSystem()
        {
            InitializeComponent();
            Loaded += AddOperatingSystem_Loaded;
        }

        public AddOperatingSystem(Models.OperatingSystem operatingSystem)
        {
            InitializeComponent();
            selectedOperatingSystem = operatingSystem;
            isEdit = true;
            Loaded += AddOperatingSystem_Loaded;
        }

        private async void AddOperatingSystem_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();

            if (isEdit && selectedOperatingSystem != null)
            {
                OperatingSystemNameBox.Text = selectedOperatingSystem.OperatingSystemName;
                PriceBox.Text = selectedOperatingSystem.OperatingSystemPrice.ToString();
                CompanyBox.SelectedValue = selectedOperatingSystem.OperatingSystemCompanyId;

                Title = "Edit OperatingSystem";
                AddBtn.Content = "Update Operating System";
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
            Models.OperatingSystem operatingSystem = new Models.OperatingSystem();
            operatingSystem.OperatingSystemName = OperatingSystemNameBox.Text;
            bool ok = int.TryParse(PriceBox.Text, out int price);
            if (ok)
                operatingSystem.OperatingSystemPrice = price;
            else
                operatingSystem.OperatingSystemPrice = -1;
            operatingSystem.OperatingSystemCompanyId = (int)CompanyBox.SelectedValue;

            operatingSystem.Validate();
            if (operatingSystem.HasErrors)
            {
                Dictionary<string, List<string>> errors = operatingSystem.AllErrors();
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

            WebClient<Models.OperatingSystem> client = new WebClient<Models.OperatingSystem>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            ok = false;

            if (isEdit)
            {
                operatingSystem.OperatingSystemId = selectedOperatingSystem.OperatingSystemId;
                client.Path = "api/Manager/EditOperatingSystem";
                ok = await client.PostAsync(operatingSystem);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Operating System Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Operating System");
                }
            }
            else
            {
                client.Path = "api/Manager/AddOperatingSystem";
                ok = await client.PostAsync(operatingSystem);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Operating System Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add Operating System");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}