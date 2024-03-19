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

namespace HospitalAdministration.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (App.User.Role > 2)
            {
                btnPatientRecordsList.Visibility = Visibility;
                btnAddDirection.Visibility = Visibility;
            }
            if (App.User.Role >= 3)
            {
                btnAddPatient.Visibility = Visibility;
            }
        }

        private void btnScheduleList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScheduleListPage());
        }

        private void btnPatientRecordsList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientRecordsListPage());
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatientPage());
        }

        private void btnAddDirection_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDirectionPage());
        }
    }
}
