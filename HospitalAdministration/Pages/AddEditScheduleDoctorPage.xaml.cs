using HospitalAdministration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddEditScheduleDoctorPage.xaml
    /// </summary>
    public partial class AddEditScheduleDoctorPage : Page
    {
        MedDbEntities dbContext;
        List<D_Doctor> doctors;
        private D_DoctorWorkSchedule _currentDoctorSchedule;
        DateTime date;
        TimeSpan timeStart;
        TimeSpan timeEnd;
        public AddEditScheduleDoctorPage()
        {
            InitializeComponent();
            Load();
        }

        public AddEditScheduleDoctorPage(D_DoctorWorkSchedule doctorSchedule)
        {
            InitializeComponent();
            Load();
            if (doctorSchedule != null)
            {
                _currentDoctorSchedule = doctorSchedule;
                DataContext = doctorSchedule;
            }
        }

        public void Load()
        {
            dbContext = App.GetContext();
            doctors = dbContext.D_Doctor.ToList();
            CbDoctor.ItemsSource = doctors;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            timeStart = TimeSpan.Parse(timeStart.ToString());
            timeEnd = TimeSpan.Parse(timeEnd.ToString());
            date = (DateTime)DpWorkDate.SelectedDate;
            if (_currentDoctorSchedule == null)
            {
                _currentDoctorSchedule = new D_DoctorWorkSchedule()
                {
                    Date = date,
                    D_Doctor = CbDoctor.SelectedItem as D_Doctor,
                    TimeBeginDay = timeStart,
                    TimeEndDay = timeEnd
                };
                var _currentDoctorScheduleHistory = new D_DoctorWorkSchedule_History()
                {
                    Id = _currentDoctorSchedule.Id,
                    Date = date,
                    D_Doctor = CbDoctor.SelectedItem as D_Doctor,
                    TimeBeginDay = timeStart,
                    TimeEndDay = timeEnd,
                    DateChange = DateTime.Now.Date,
                    TimeChange = DateTime.Now.TimeOfDay,
                    TypeChange = 1
                };
                dbContext.D_DoctorWorkSchedule.Add(_currentDoctorSchedule);
                dbContext.D_DoctorWorkSchedule_History.Add(_currentDoctorScheduleHistory);
            }
            else
            {
                var _currentDoctorScheduleHistory = new D_DoctorWorkSchedule_History()
                {
                    Id = _currentDoctorSchedule.Id,
                    Date = date,
                    D_Doctor = CbDoctor.SelectedItem as D_Doctor,
                    TimeBeginDay = (DataContext as D_DoctorWorkSchedule).TimeBeginDay,
                    TimeEndDay = (DataContext as D_DoctorWorkSchedule).TimeEndDay,
                    DateChange = DateTime.Now.Date,
                    TimeChange = DateTime.Now.TimeOfDay,
                    TypeChange = 2
                };
                dbContext.D_DoctorWorkSchedule_History.Add(_currentDoctorScheduleHistory);
            }
            if (timeStart != null && timeEnd != null && date != null)
            {
                dbContext.SaveChanges();
                MessageBox.Show("Запись успешно сохранена!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();    
            }
        }
    }
}
