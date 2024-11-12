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
    /// Логика взаимодействия для AddMedicalRecordPage.xaml
    /// </summary>
    public partial class AddMedicalRecordPage : Page
    {
        private int _appId = 0;
        public AddMedicalRecordPage(int appId)
        {
            InitializeComponent();

            _appId = appId; 
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_appId != 0 && !string.IsNullOrEmpty(DescBox.Text))
            {
                var med = new MedicalRecord
                {
                    AppointmentId = _appId,
                    Description = DescBox.Text,
                };

                App.db.MedicalRecords.Add(med);
                await App.db.SaveChangesAsync();

                NavigationService.GoBack();
            }
        }
    }
}
