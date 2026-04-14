using System.Collections.Generic;
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
            storage.StorageName = StorageNameBox.Text;
            storage.StorageSize = SizeBox.Text;
            storage.StorageSpeed = SpeedBox.Text;
            storage.StorageType = (int)TypeBox.SelectedValue;
            storage.StoragePrice = int.Parse(PriceBox.Text);
            storage.StorageCompanyId = (int)CompanyBox.SelectedValue;

            WebClient<Storage> client = new WebClient<Storage>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                storage.StorageId = selectedStorage.StorageId;
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