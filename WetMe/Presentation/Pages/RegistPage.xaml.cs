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
using WetMe.Data.Remote;

namespace WetMe.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistPage.xaml
    /// </summary>
    public partial class RegistPage : Page
    {
        public RegistPage()
        {
            InitializeComponent();
        }

        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Client()
            {
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Phone = PhoneBox.Text,
                Password = PasswordBox.Password,
            };

            await AddClient(client);

            MessageBox.Show("Успех");
        }

        private async Task AddClient(Client client)
        {
            App.db.Clients.Add(client);
            await App.db.SaveChangesAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
