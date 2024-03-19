using HospitalAdministration.Entities;
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
    /// Логика взаимодействия для AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        MedDbEntities dbContext;
        D_Patient patient;
        D_Patient savePatient;
        public AddPatientPage()
        {
            InitializeComponent();
            dbContext = App.GetContext();
            //DataContext = dbContext.D_Patient;
            CbGender.ItemsSource = dbContext.D_Gender.ToList();
        }

        private void Birthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddDiagnos_Click(object sender, RoutedEventArgs e)
        {
            if (savePatient == null)
            {
                patient = new D_Patient()
                {
                    Medcard = TbMedcard.Text,
                    MedcardDate = DpMedcardDate.SelectedDate,
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    SecondName = TbSecondName.Text,
                    Birthday = DpBirthday.SelectedDate,
                    D_Gender = CbGender.SelectedItem as D_Gender,
                    Phone = TbPhone.Text,
                    Email = TbEmail.Text
                };
                savePatient = dbContext.D_Patient.Add(patient);
                dbContext.SaveChanges();
            }
            
            NavigationService.Navigate(new AddDiagnosis(savePatient));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (savePatient != null)
            {
                MessageBox.Show("Информация сохранена!");
                
                return;
            }


            if (savePatient == null)
            {
                patient = new D_Patient()
                {
                    FirstName = TbFirstName.Text,
                    LastName = TbLastName.Text,
                    SecondName = TbSecondName.Text,
                    Birthday = DpBirthday.SelectedDate,
                    D_Gender = CbGender.SelectedItem as D_Gender,
                    Phone = TbPhone.Text,
                    Email = TbEmail.Text
                };
                if (MessageBox.Show($"Вы не указали историю пациента {patient.FIO}, хотите продолжить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dbContext.D_Patient.Add(patient);
                    MessageBox.Show("Информация сохранена!");
                }
            }
        }

        private void DpMedcardDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (savePatient!=null)
            {
                DgDiagnosisHistory.ItemsSource = dbContext.D_HistoryDiagnosis.Where(x=>x.D_Patient.Id == savePatient.Id).ToList();
            }
        }
    }
}
