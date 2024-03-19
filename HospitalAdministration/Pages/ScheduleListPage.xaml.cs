using HospitalAdministration.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для ScheduleListPage.xaml
    /// </summary>
    public partial class ScheduleListPage : Page
    {
        MedDbEntities dbContext;
        List<D_DoctorWorkSchedule> doctorWorkSchedules = new List<D_DoctorWorkSchedule>();
        D_Category doctorCategory;
        int lastDate;
        DateTime startDateTime;
        DateTime endDateTime;

        public ScheduleListPage()
        {
            InitializeComponent();
            dbContext = App.GetContext();
            List<D_Category> categories = dbContext.D_Category.ToList();
            categories.Insert(0, new D_Category() { Name = "Все" });
            CbDoctorCategory.ItemsSource = categories.ToList();
            CbDoctorCategory.SelectedIndex = 0;



            if (App.User.Role == 4)
            {
                btnApprove.Visibility = Visibility.Visible;
            }
            if (App.User.Role >= 3)
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            if (App.User.Role >= 2)
            {
                DgWorked.IsReadOnly = false;
                CmCommand.IsEnabled = true;
                btnSave.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnOpen.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Visible;
            }
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorCategory = e.AddedItems[0] as D_Category;
            Update();
        }

        private void Update()
        {
            dbContext = App.GetContext();
            doctorWorkSchedules = dbContext.D_DoctorWorkSchedule.ToList();

            if (!string.IsNullOrWhiteSpace(TbSearch.Text))
            {
                doctorWorkSchedules = doctorWorkSchedules.Where(x => x.D_Doctor.FIO.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            }

            if (CbDoctorCategory.SelectedIndex != 0)
            {
                doctorWorkSchedules = doctorWorkSchedules.Where(x => x.D_Doctor.Category == doctorCategory.Id).ToList();
            }
            else
            {
                doctorWorkSchedules = doctorWorkSchedules.ToList();
            }

            doctorWorkSchedules = doctorWorkSchedules.Where(x => x.Date >= startDateTime && x.Date <= endDateTime).ToList();

            DgDoctorWorkSchedule.ItemsSource = doctorWorkSchedules;
        }

        private void Calendar_DisplayDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lastDate = e.AddedItems.Count - 1;
            startDateTime = (DateTime)e.AddedItems[0];
            endDateTime = (DateTime)e.AddedItems[lastDate];

            Update();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Save();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (App.User.Role < 2)
            {
                return;
            }
            if (DgDoctorWorkSchedule.SelectedItem is D_DoctorWorkSchedule select)
            {
                NavigationService.Navigate(new AddEditScheduleDoctorPage(select));
                return;
            }
            NavigationService.Navigate(new AddEditScheduleDoctorPage());
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditScheduleDoctorPage());
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (App.User.Role < 2)
            {
                return;
            }
            D_DoctorWorkSchedule selectedItem = DgDoctorWorkSchedule.SelectedItem as D_DoctorWorkSchedule;
            if (selectedItem != null)
            {
                if (MessageBox.Show($"Удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dbContext.D_DoctorWorkSchedule.Remove(selectedItem);
                    var _currentDoctorScheduleHistory = new D_DoctorWorkSchedule_History()
                    {
                        Id = selectedItem.Id,
                        Date = selectedItem.Date,
                        D_Doctor = selectedItem.D_Doctor,
                        TimeBeginDay = selectedItem.TimeBeginDay,
                        TimeEndDay = selectedItem.TimeEndDay,
                        DateChange = DateTime.Now.Date,
                        TimeChange = DateTime.Now.TimeOfDay,
                        TypeChange = 3
                    };
                    dbContext.D_DoctorWorkSchedule_History.Add(_currentDoctorScheduleHistory);
                    dbContext.SaveChanges();
                    Update();
                }
            }
        }

        private void OpenAdd()
        {

        }

        private void Save()
        {
            if (App.User.Role < 2)
            {
                return;
            }
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

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (App.User.Role < 2)
            {
                return;
            }
            List<D_DoctorWorkSchedule> selectedSchedule = DgDoctorWorkSchedule.SelectedItems.Cast<D_DoctorWorkSchedule>().ToList();
            StringBuilder sb = new StringBuilder();
            if (selectedSchedule != null)
            {
                foreach (var schedule in selectedSchedule)
                {
                    schedule.isApproved = true;
                }
                if (MessageBox.Show($"Следующие расписание и {selectedSchedule.Count} записей будут утверждены.", 
                    "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    Save();
                }
            }
        }
    }
}
