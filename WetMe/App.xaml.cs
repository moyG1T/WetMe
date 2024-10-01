using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WetMe.Data.Remote;

namespace WetMe
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static VeterinaryClinicEntities db = new VeterinaryClinicEntities();
    }
}
