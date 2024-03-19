using HospitalPatitentRegApp.Pages;
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

namespace HospitalPatitentRegApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.NavigationService.CanGoBack)
            {
                App.User = null;
                MainFrame.NavigationService.GoBack();
                this.UpdateLayout();
            }
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            if (App.User != null)
            {
                btnLogout.Visibility = Visibility.Visible;
            }
            else
            {
                btnLogout.Visibility = Visibility.Collapsed;
            }
        }
    }
}
