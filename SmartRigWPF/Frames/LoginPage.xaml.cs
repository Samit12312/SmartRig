using ApiClient;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
namespace SmartRigWPF.Frames
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {

        bool isLogin = false;

        public bool IsLogin
        {
            get { return isLogin; }
           
        }   
        public LoginPage()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UserValidationThingy user = new UserValidationThingy();
            user.UserEmail = EmailTextBox.Text;
            user.UserPassword = PasswordBox.Password;
            if (EmailTextBox.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("Enter email and password");
                return;
            }
            user.Validate();
            if (user.HasErrors)
            {
                Dictionary<string, List<string>> errors = user.AllErrors();
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
            WebClient<LoginResponse> client = new WebClient<LoginResponse>();
            client.Schema = "http";
            client.Host = "localhost";
            client.Port = 5195;
            client.Path = "api/Guest/Login";
            client.AddParameter("email", EmailTextBox.Text);
            client.AddParameter("password", PasswordBox.Password);
            
            LoginResponse response = client.Get();

            if (response != null && response.Success && response.Manager)
            {
                this.isLogin = true;

                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

                if (mainWindow != null)
                {
                    mainWindow.LoginSuccess(response.UserName);
                }
            }
            else if (response != null && response.Success && response.Manager == false)
            {
                MessageBox.Show("This user is not a manager");
            }
            else
            {
                MessageBox.Show("Invalid email or password");
            }
        }
    }
}
