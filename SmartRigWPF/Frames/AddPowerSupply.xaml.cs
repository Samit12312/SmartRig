using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddPowerSupply : Window
    {
        bool isEdit = false;
        PowerSupply selectedPowerSupply = null;

        public AddPowerSupply()
        {
            InitializeComponent();
            Loaded += AddPowerSupply_Loaded;
        }

        public AddPowerSupply(PowerSupply powerSupply)
        {
            InitializeComponent();
            selectedPowerSupply = powerSupply;
            isEdit = true;
            Loaded += AddPowerSupply_Loaded;
        }

        private async void AddPowerSupply_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();

            if (isEdit && selectedPowerSupply != null)
            {
                PowerSupplyNameBox.Text = selectedPowerSupply.PowerSupplyName;
                WattBox.Text = selectedPowerSupply.PowerSupplyWatt.ToString();
                PriceBox.Text = selectedPowerSupply.PowerSupplyPrice.ToString();
                CompanyBox.SelectedValue = selectedPowerSupply.PowerSupplyCompanyId;

                Title = "Edit PowerSupply";
                AddBtn.Content = "Update Power Supply";
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
            PowerSupply powerSupply = new PowerSupply();
            powerSupply.PowerSupplyName = PowerSupplyNameBox.Text;
            powerSupply.PowerSupplyWatt = int.Parse(WattBox.Text);
            powerSupply.PowerSupplyPrice = int.Parse(PriceBox.Text);
            powerSupply.PowerSupplyCompanyId = (int)CompanyBox.SelectedValue;

            WebClient<PowerSupply> client = new WebClient<PowerSupply>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                powerSupply.PowerSupplyId = selectedPowerSupply.PowerSupplyId;
                client.Path = "api/Manager/EditPowerSupply";
                ok = await client.PostAsync(powerSupply);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Power Supply Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Power Supply");
                }
            }
            else
            {
                client.Path = "api/Manager/AddPowerSupply";
                ok = await client.PostAsync(powerSupply);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Power Supply Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add Power Supply");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}