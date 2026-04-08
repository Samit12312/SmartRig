using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;
using Models.ViewModels;

namespace SmartRigWPF.Frames
{
    public partial class AddCase : Window
    {
        bool isEdit;
        Models.Case selectedCase;

        public AddCase()
        {
            InitializeComponent();
            GetNewCaseViewModel();
        }

        public AddCase(Models.Case caseItem)
        {
            InitializeComponent();
            this.selectedCase = caseItem;
            isEdit = true;
            GetNewCaseViewModel();
        }

        private async Task GetNewCaseViewModel()
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

                if (isEdit && selectedCase != null)
                {
                    CaseNameBox.Text = selectedCase.CaseName;
                    PriceBox.Text = selectedCase.CasePrice.ToString();
                    CompanyBox.SelectedValue = selectedCase.CaseCompanyId;

                    this.Title = "Edit Case";
                    AddBtn.Content = "Update Case";
                }
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Models.Case caseItem = new Models.Case();
            caseItem.CaseName = CaseNameBox.Text;
            caseItem.CasePrice = int.Parse(PriceBox.Text);
            caseItem.CaseCompanyId = (int)CompanyBox.SelectedValue;

            WebClient<Models.Case> client = new WebClient<Models.Case>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                caseItem.CaseId = selectedCase.CaseId;
                client.Path = "api/Manager/EditCase";
                ok = await client.PostAsync(caseItem);

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("Case Updated");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Case");
                }
            }
            else
            {
                client.Path = "api/Manager/AddCase";
                ok = await client.PostAsync(caseItem);

                if (ok)
                {
                    this.DialogResult = true;
                    MessageBox.Show("Case Added");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add Case");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}