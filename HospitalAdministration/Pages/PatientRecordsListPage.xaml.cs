using HospitalAdministration.Entities;
using HospitalAdministration.MedDbDataSetTableAdapters;
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

namespace HospitalAdministration.Pages
{
    /// <summary>
    /// Логика взаимодействия для PatientRecordsListPage.xaml
    /// </summary>
    public partial class PatientRecordsListPage : Page
    {
        MedDbEntities dbContext;
        List<D_ReceptionSchedule> patientRecords = new List<D_ReceptionSchedule>();
        D_Category doctorCategory;
        int lastDate;
        DateTime startDateTime;
        DateTime endDateTime;

        public PatientRecordsListPage()
        {
            InitializeComponent();
            dbContext = App.GetContext();
            List<D_Category> categories = dbContext.D_Category.ToList();
            categories.Insert(0, new D_Category() { Name = "Все" });
            Update();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Calendar_DisplayDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startDateTime = (DateTime)e.AddedItems[0];
            endDateTime = (DateTime)e.AddedItems[e.AddedItems.Count-1];
            Update();
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DgPatientRecords.SelectedItem is D_ReceptionSchedule reception)
            {
                NavigationService.Navigate(new AddEditPatientRecord(reception));
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
            Update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPatientRecord());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update()
        {
            dbContext = App.GetContext();
            patientRecords = dbContext.D_ReceptionSchedule.ToList();

            if (!string.IsNullOrWhiteSpace(TbSearch.Text))
            {
                string search = TbSearch.Text.ToLower();
                patientRecords = patientRecords.Where(x => x.D_Patient.FIO.ToLower().Contains(search)).ToList();

            }
            if (startDateTime != null && endDateTime != null)
            {
                patientRecords = patientRecords.Where(x=>x.Date >= startDateTime && x.Date <= endDateTime).ToList();
            }
            

            //if (CbDoctorCategory.SelectedIndex != 0)
            //{
            //    patientRecords = patientRecords.Where(x =>
            //    x.D_DoctorGroup.FirstOrDefault(d => d.isDoctorChief == true &&
            //        d.D_Doctor.Category == (CbDoctorCategory.SelectedItem as D_Category).Id).D_Doctor.Category == (CbDoctorCategory.SelectedItem as D_Category).Id).ToList();

            //}
            DgPatientRecords.ItemsSource = patientRecords.ToList();
        }

        private void Save()
        {
            try
            {
                dbContext.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успеншо", MessageBoxButton.OK, MessageBoxImage.Information);
                Update();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
