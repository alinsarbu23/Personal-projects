using Supermarket.ViewModels;
using System.Windows;

namespace Supermarket.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel loginViewModel;

        public LoginWindow()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (loginViewModel.Login(username, password))
            {
               
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username, password, or insufficient privileges.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
