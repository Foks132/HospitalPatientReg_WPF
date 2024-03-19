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
    /// Логика взаимодействия для AddDiagnosis.xaml
    /// </summary>
    public partial class AddDiagnosis : Page
    {
        D_Patient currentPatient;
        MedDbEntities dbContext;
        public AddDiagnosis()
        {
            InitializeComponent();
        }

        public AddDiagnosis(D_Patient patient)
        {
            InitializeComponent();
            currentPatient = patient;
            dbContext = App.GetContext();
            //DataContext = dbContext.D_HistoryDiagnosis;
            CbDiagnos.ItemsSource = dbContext.D_Diagnosis.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var diagnos = new D_HistoryDiagnosis()
                {
                    Patient = currentPatient.Id,
                    Date = DpDate.SelectedDate,
                    D_Diagnosis = CbDiagnos.SelectedItem as D_Diagnosis,
                    Description = TbDiscription.Text
                };
                dbContext.D_HistoryDiagnosis.Add(diagnos);
                dbContext.SaveChanges();
                MessageBox.Show("История пациента занесена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
