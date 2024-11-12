using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для VetsClient.xaml
    /// </summary>
    public partial class VetsClient : Page, INotifyPropertyChanged
    {
        public Vet Vet { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public VetsClient()
        {
            InitializeComponent();

            DataContext = this;

            Loaded += OnLoaded;
        }

        ~VetsClient()
        {
            Loaded -= OnLoaded;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.IsVet)
            {
                await LoadContent();
            }
        }

        private async Task LoadContent()
        {
            int vetId = Settings.Default.LoggedUser;

            Vet = await App.db.Vets.FirstOrDefaultAsync(it => it.Id == vetId);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vet)));

            var list = await App.db.Appointments.Where(it => it.VetId == vetId && it.IsRemoved == false).ToListAsync();
            Appointments = new ObservableCollection<Appointment>(list);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Appointments)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = VetsClientList.SelectedItem as Appointment;
            if (app != null)
            {
                NavigationService.Navigate(new AddMedicalRecordPage(app.Id));
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var app = VetsClientList.SelectedItem as Appointment;
            if (app != null)
            {
                app.IsRemoved = true;
                await App.db.SaveChangesAsync();

                Appointments.Remove(app);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var list = await App.db.Appointments.Where(it => it.VetId == Vet.Id).ToListAsync();

            Appointments = new ObservableCollection<Appointment>(list);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Appointments)));
        }
    }
}
