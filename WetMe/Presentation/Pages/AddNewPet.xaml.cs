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
using WetMe.Data.Remote;
using WetMe.Properties;

namespace WetMe.Presentation.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewPet.xaml
    /// </summary>
    public partial class AddNewPet : Page
    {
        public string FirstName { get; set; }
        public AddNewPet()
        {
            InitializeComponent();

            FirstName = App.db.Clients.FirstOrDefault(it => it.ClientID == Settings.Default.LoggedUser)?.FirstName ?? "";
            
            GenderCB.ItemsSource = App.db.PetGenders.ToList();
            GenderCB.DisplayMemberPath = "Name";
            SpeciesCB.ItemsSource = App.db.Species.ToList();
            SpeciesCB.DisplayMemberPath = "Title";
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.LoggedUser != 0 && !string.IsNullOrEmpty(NameBox.Text))
            {
                var pet = new Pet()
                {
                    GenderId = (GenderCB.SelectedItem as PetGender).Id,
                    OwnerId = Settings.Default.LoggedUser,
                    Name = NameBox.Text,
                    Birthdate = DateDP.SelectedDate,
                    SpeciesId = (SpeciesCB.SelectedItem as Species).Id,
                };

                App.db.Pets.Add(pet);
                await App.db.SaveChangesAsync();

                NavigationService.Navigate(new ClientPage());
            }
        }
    }
}
