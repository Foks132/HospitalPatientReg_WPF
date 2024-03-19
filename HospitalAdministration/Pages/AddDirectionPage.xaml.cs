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
    /// Логика взаимодействия для AddDirectionPage.xaml
    /// </summary>
    public partial class AddDirectionPage : Page
    {
        D_Patient patient;
        MedDbEntities dbContext;
        public AddDirectionPage()
        {
            InitializeComponent();
            dbContext = App.GetContext();
            CbDoctor.ItemsSource = dbContext.D_Doctor.ToList();
        }

        private void TbMedcard_TextChanged(object sender, TextChangedEventArgs e)
        {
            CbPatient.ItemsSource = dbContext.D_Patient.Where(x => x.Medcard.Contains(TbMedcard.Text)).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            patient = CbPatient.SelectedItem as D_Patient;
            try
            {
                var therapeutic = new TherapeuticPatient()
                {
                    D_Patient = CbPatient.SelectedItem as D_Patient,
                    D_Doctor = CbDoctor.SelectedItem as D_Doctor
                };

                dbContext.TherapeuticPatients.Add(therapeutic);
                dbContext.SaveChanges();
                MessageBox.Show($"Выдано направление к {therapeutic.D_Doctor.DoctorWithCategory} пациенту {therapeutic.D_Patient.FIO}", "Направление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
