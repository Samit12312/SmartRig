using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using ApiClient;

namespace SmartRigWPF.Frames
{
    /// <summary>
    /// Interaction logic for ManageReports.xaml
    /// </summary>
    public partial class ManageReports : UserControl
    {
        public ManageReports()
        {
            InitializeComponent();
            LoadMostSellingComputer();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select both start and end dates.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be before end date.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            await GetTotalRevenue(startDate, endDate);
        }

        private async Task GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                WebClient<decimal> client = new WebClient<decimal>();
                client.Schema = "http";
                client.Host = "localhost";
                client.Port = 5195;
                client.Path = "api/Manager/GetTotalRevenue";
                client.AddParameter("fromDate", startDate.ToString("MM/dd/yyyy"));
                client.AddParameter("toDate", endDate.ToString("MM/dd/yyyy"));

                decimal totalIncome = await client.GetAsync();

                TotalIncomeText.Text = $"${totalIncome:N2}";
                DateRangeText.Text = $"{startDate:MM/dd/yyyy} - {endDate:MM/dd/yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading revenue: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadMostSellingComputer()
        {
            try
            {
                WebClient<CartComputer> client = new WebClient<CartComputer>();
                client.Schema = "http";
                client.Host = "localhost";
                client.Port = 5195;
                client.Path = "api/Manager/GetMostSoldComputer";

                CartComputer mostSold = await client.GetAsync();

                if (mostSold != null)
                {
                    ComputerIdText.Text = mostSold.ComputerId.ToString();
                    ComputerNameText.Text = mostSold.ComputerName;
                    ComputerPriceText.Text = $"${mostSold.ComputerPrice:N2}";

                    string imageUrl = $"http://localhost:5195/Images/Computers/{mostSold.ComputerPicture}";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
                    bitmap.EndInit();
                    MostSellingComputerImage.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading most selling computer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}