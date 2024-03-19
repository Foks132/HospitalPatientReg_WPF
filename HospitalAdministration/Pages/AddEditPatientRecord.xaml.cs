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
    /// Логика взаимодействия для AddEditPatientRecord.xaml
    /// </summary>
    public partial class AddEditPatientRecord : Page
    {
        MedDbEntities dbContext;
        D_Category selectCategory;
        List<D_Doctor> doctors = new List<D_Doctor>();
        List<D_Patient> patients = new List<D_Patient>();
        private D_ReceptionSchedule _currentReceptionSchedule;
        DateTime date;
        TimeSpan time;
        public AddEditPatientRecord()
        {
            InitializeComponent();
            Load();
            CbCategory.ItemsSource = dbContext.D_Category.ToList();

        }

        public AddEditPatientRecord(D_ReceptionSchedule receptionSchedule)
        {
            InitializeComponent();
            Load();
            DataContext = receptionSchedule;
            _currentReceptionSchedule = receptionSchedule;
            CbCategory.ItemsSource = dbContext.D_Category.ToList();
            CbDoctor.ItemsSource = dbContext.D_Doctor.ToList();
        }

        private void Load()
        {
            dbContext = App.GetContext();
        }

        private void CbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectCategory = e.AddedItems[0] as D_Category;

            Update();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var doctor = CbDoctor.SelectedItem as D_Doctor;
            if (_currentReceptionSchedule == null)
            {
                dbContext.D_ReceptionSchedule.Add(new D_ReceptionSchedule()
                {
                    D_Patient = CbPatient.SelectedItem as D_Patient,
                    Date = (DateTime)DpWorkDate.SelectedDate,
                    Time = TimeSpan.Parse(TbTime.Text),
                    DoctorGroupId = dbContext.D_DoctorGroup.FirstOrDefault(x=>x.D_Doctor.Id == doctor.Id && x.isDoctorChief == true).Id,
                });
            }
            _currentReceptionSchedule.DoctorGroupId = dbContext.D_DoctorGroup.FirstOrDefault(x => x.D_Doctor.Id == doctor.Id && x.isDoctorChief == true).Id;
            dbContext.SaveChanges();
            NavigationService.GoBack();
        }


        private void TbMedcard_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void CbPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            if (!string.IsNullOrWhiteSpace(TbMedcard.Text))
            {
                patients = dbContext.D_Patient.Where(x => x.Medcard.Contains(TbMedcard.Text)).ToList();
                CbPatient.ItemsSource = patients.ToList();
            }

            if (selectCategory != null)
            {
                var doctorsGroupChief = dbContext.D_DoctorGroup.Where(x => x.D_Doctor.Category == selectCategory.Id &&
                    x.isDoctorChief == true).ToList();
                if (doctorsGroupChief != null)
                {
                    doctors.Clear();
                    foreach (var doctorChief in doctorsGroupChief)
                    {
                        doctors.Add(doctorChief.D_Doctor);
                    }
                }
            }
            if (doctors != null)
            {
                CbDoctor.ItemsSource = doctors.ToList();
            }

        }
    }
}
