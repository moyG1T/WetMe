using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WetMe.Properties;

namespace WetMe.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            string phone = PhoneBox.Text;
            string password = PasswordBox.Password;

            if (await SearchUser(phone, password))
            {
                NavigationService.Navigate(new VetsClient());
            }
        }

        private async Task<bool> SearchUser(string phone, string password)
        {
            var client = await App.db.Clients.FirstOrDefaultAsync(c => c.Phone == phone && c.Password == password);
            var vet = await App.db.Vets.FirstOrDefaultAsync(c => c.Phone == phone && c.Password == password);

            if (client is null && vet is null)
            {
                return false;
            }
            else if (vet is null)
            {
                Settings.Default.LoggedUser = client.ClientID;
                Settings.Default.Save();

                return true;
            }
            else
            {
                Settings.Default.LoggedUser = vet.Id;
                Settings.Default.IsVet = true;
                Settings.Default.Save();

                return true;
            }
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistPage());
        }
    }
}
