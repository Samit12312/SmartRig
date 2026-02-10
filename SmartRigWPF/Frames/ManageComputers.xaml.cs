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
    /// Interaction logic for ManageComputers.xaml
    /// </summary>
    public partial class ManageComputers : UserControl
    {
        List<Computer> computers = new List<Computer>();
        public ManageComputers()
        {
            InitializeComponent();
            GetComputers();
        }
        private async Task GetComputers()
        {
            WebClient<List<Computer>> client = new WebClient<List<Computer>>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 7249;
            client.Path = "api/Manager/GetAllComputers";
            this.computers = await client.GetAsync();

            this.DataContext = this.computers;
            listView.ItemsSource = this.computers;  

        }
    }
}
