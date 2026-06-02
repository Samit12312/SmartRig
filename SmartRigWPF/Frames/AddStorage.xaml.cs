using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddStorage : Window
    {
        bool isEdit = false;
        Storage selectedStorage = null;

        public AddStorage()
        {
            InitializeComponent();
            Loaded += AddStorage_Loaded;
        }

        public AddStorage(Storage storage)
        {
            InitializeComponent();
            selectedStorage = storage;
            isEdit = true;
            Loaded += AddStorage_Loaded;
        }

        private async void AddStorage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();
            await LoadTypes();

            if (isEdit && selectedStorage != null)
            {
                StorageNameBox.Text = selectedStorage.StorageName;
                SizeBox.Text = selectedStorage.StorageSize.ToString();
                SpeedBox.Text = selectedStorage.StorageSpeed.ToString();
                TypeBox.SelectedValue = selectedStorage.StorageType;
                PriceBox.Text = selectedStorage.StoragePrice.ToString();
                CompanyBox.SelectedValue = selectedStorage.StorageCompanyId;

                Title = "Edit Storage";
                AddBtn.Content = "Update Storage";
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

        private async Task LoadTypes()
        {
            WebClient<List<Models.Type>> client = new WebClient<List<Models.Type>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Manager/GetStorageTypes";

            List<Models.Type> types = await client.GetAsync();

            if (types != null)
            {
                TypeBox.ItemsSource = types;
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage();

            if (isEdit && selectedStorage != null)
            {
                storage.StorageId = selectedStorage.StorageId;
            }

            storage.StorageName = StorageNameBox.Text;
            storage.StorageSize = SizeBox.Text;
            storage.StorageSpeed = SpeedBox.Text;

            bool ok = int.TryParse(PriceBox.Text, out int price);

            if (ok)
            {
                storage.StoragePrice = price;
            }
            else
            {
                storage.StoragePrice = -1;
            }

            if (TypeBox.SelectedValue == null)
            {
                storage.StorageType = 0;
            }
            else
            {
                storage.StorageType = (int)TypeBox.SelectedValue;
            }

            if (CompanyBox.SelectedValue == null)
            {
                storage.StorageCompanyId = 0;
            }
            else
            {
                storage.StorageCompanyId = (int)CompanyBox.SelectedValue;
            }

            storage.Validate();

            if (storage.HasErrors)
            {
                Dictionary<string, List<string>> errors = storage.AllErrors();
                StringBuilder errorMessage = new StringBuilder();

                foreach (var error in errors)
                {
                    errorMessage.AppendLine(error.Key + ":");

                    foreach (var errorDetail in error.Value)
                    {
                        errorMessage.AppendLine(" - " + errorDetail);
                    }
                }

                MessageBox.Show(errorMessage.ToString(), "Correct next errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            WebClient<Storage> client = new WebClient<Storage>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            ok = false;

            if (isEdit)
            {
                client.Path = "api/Manager/EditStorage";
                ok = await client.PostAsync(storage);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Storage Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Storage");
                }
            }
            else
            {
                client.Path = "api/Manager/AddStorage";
                ok = await client.PostAsync(storage);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("Storage Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add Storage");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}