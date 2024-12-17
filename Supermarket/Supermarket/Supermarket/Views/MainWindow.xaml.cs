using System.Windows;
using Supermarket.Views;

namespace Supermarket
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdministratorButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            if (loginWindow.ShowDialog() == true)
            {
                AdministratorWindow adminWindow = new AdministratorWindow();
                adminWindow.ShowDialog();
            }
        }

        private void CashierButton_Click(object sender, RoutedEventArgs e)
        {
            CasierWindow casierWindow = new CasierWindow(); 
            casierWindow.ShowDialog();
        }
    }
}
