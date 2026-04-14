using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ApiClient;
using Models;

namespace SmartRigWPF.Frames
{
    public partial class AddMotherBoard : Window
    {
        bool isEdit = false;
        MotherBoard selectedMotherBoard = null;

        public AddMotherBoard()
        {
            InitializeComponent();
            Loaded += AddMotherBoard_Loaded;
        }

        public AddMotherBoard(MotherBoard motherBoard)
        {
            InitializeComponent();
            selectedMotherBoard = motherBoard;
            isEdit = true;
            Loaded += AddMotherBoard_Loaded;
        }

        private async void AddMotherBoard_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();

            if (isEdit && selectedMotherBoard != null)
            {
                MotherBoardNameBox.Text = selectedMotherBoard.MotherBoardName;
                PriceBox.Text = selectedMotherBoard.MotherBoardPrice.ToString();
                CompanyBox.SelectedValue = selectedMotherBoard.MotherBoardCompanyId;

                Title = "Edit MotherBoard";
                AddBtn.Content = "Update MotherBoard";
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
            MotherBoard motherBoard = new MotherBoard();
            motherBoard.MotherBoardName = MotherBoardNameBox.Text;
            motherBoard.MotherBoardPrice = int.Parse(PriceBox.Text);
            motherBoard.MotherBoardCompanyId = (int)CompanyBox.SelectedValue;

            WebClient<MotherBoard> client = new WebClient<MotherBoard>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;

            bool ok = false;

            if (isEdit)
            {
                motherBoard.MotherBoardId = selectedMotherBoard.MotherBoardId;
                client.Path = "api/Manager/EditMotherBoard";
                ok = await client.PostAsync(motherBoard);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("MotherBoard Updated");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update MotherBoard");
                }
            }
            else
            {
                client.Path = "api/Manager/AddMotherBoard";
                ok = await client.PostAsync(motherBoard);

                if (ok)
                {
                    DialogResult = true;
                    MessageBox.Show("MotherBoard Added");
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to add MotherBoard");
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}