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
using WetMe.Presentation.Pages;
using WetMe.Properties;

namespace WetMe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Settings.Default.IsVet && Settings.Default.LoggedUser != 0)
            {
                MainFrame.Navigate(new VetsClient());
            }
            else
            {
                MainFrame.Navigate(new AuthPage());
            }
        }
    }
}
