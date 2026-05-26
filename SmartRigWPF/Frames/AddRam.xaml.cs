using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddRam : Window
    {
        bool isEdit = false;
        Ram selectedRam = null;

        public AddRam()
        {
            InitializeComponent();
            Loaded += AddRam_Loaded;
        }

        public AddRam(Ram ram)
        {
            InitializeComponent();
            selectedRam = ram;
            isEdit = true;
            Loaded += AddRam_Loaded;
        }

        private async void AddRam_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();
            await LoadTypes();

            if (isEdit && selectedRam != null)
            {
                RamNameBox.Text = selectedRam.RamName;
                SizeBox.Text = selectedRam.RamSize;
                SpeedBox.Text = selectedRam.RamSpeed;
                PriceBox.Text = selectedRam.RamPrice.ToString();

                TypeBox.SelectedValue = selectedRam.RamTypeId;
                CompanyBox.SelectedValue = selectedRam.RamCompanyId;

                Title = "Edit Ram";
                AddBtn.Content = "Update RAM";
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

                if (isEdit && selectedRam != null)
                {
                    CompanyBox.SelectedItem = companies.FirstOrDefault(x => x.CompanyId == selectedRam.RamCompanyId);
                }
            }
        }

        private async Task LoadTypes()
        {
            WebClient<List<Models.Type>> client = new WebClient<List<Models.Type>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetRamTypes";

            List<Models.Type> types = await client.GetAsync();

            if (types != null)
            {
                TypeBox.ItemsSource = types;

                if (isEdit && selectedRam != null)
                {
                    TypeBox.SelectedItem = types.FirstOrDefault(x => x.TypeId == selectedRam.RamTypeId);
                }
            }
        }

        private int GetSelectedCompanyId()
        {
            if (CompanyBox.SelectedValue != null && int.TryParse(CompanyBox.SelectedValue.ToString(), out int id))
                return id;

            if (CompanyBox.SelectedItem is Company company)
                return company.CompanyId;

            if (isEdit && selectedRam != null)
                return selectedRam.RamCompanyId;

            return 0;
        }

        private int GetSelectedTypeId()
        {
            if (TypeBox.SelectedValue != null && int.TryParse(TypeBox.SelectedValue.ToString(), out int id))
                return id;

            if (TypeBox.SelectedItem is Models.Type type)
                return type.TypeId;

            if (isEdit && selectedRam != null)
                return selectedRam.RamTypeId;

            return 0;
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Ram ram = new Ram();

            ram.RamName = RamNameBox.Text;
            ram.RamSize = SizeBox.Text;
            ram.RamSpeed = SpeedBox.Text;
            ram.RamTypeId = GetSelectedTypeId();
            ram.RamCompanyId = GetSelectedCompanyId();

            bool priceOk = int.TryParse(PriceBox.Text, out int price);

            if (priceOk)
                ram.RamPrice = price;
            else
                ram.RamPrice = -1;

            if (isEdit && selectedRam != null)
                ram.RamId = selectedRam.RamId;

            ram.Validate();

            if (ram.HasErrors)
            {
                Dictionary<string, List<string>> errors = ram.AllErrors();
                StringBuilder errorMessage = new StringBuilder();

                foreach (var error in errors)
                {
                    errorMessage.AppendLine($"{error.Key}:");

                    foreach (var errorDetail in error.Value)
                    {
                        errorMessage.AppendLine(" - " + errorDetail);
                    }
                }

                MessageBox.Show(errorMessage.ToString(), "Correct next errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WebClient<Ram> client = new WebClient<Ram>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                client.Path = "api/Manager/EditRam";
                ok = await client.PostAsync(ram);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("RAM Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update RAM");
                }
            }
            else
            {
                client.Path = "api/Manager/AddRam";
                ok = await client.PostAsync(ram);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("RAM Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add RAM");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}