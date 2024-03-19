using HospitalPatitentRegApp.Entities;
using HospitalPatitentRegApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalPatitentRegApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для HospitalRoomsPage.xaml
    /// </summary>
    public partial class HospitalRoomsPage : Page
    {
        D_HospitalBedFund currentHospitalBedFund = null;
        List<FrameworkElement> bedElements = new List<FrameworkElement>();
        List<D_HospitalBedFund> dbContext = App.GetContext().D_HospitalBedFund.ToList();
        public HospitalRoomsPage()
        {
            InitializeComponent();

            bedElements = new DbRoomBedRows(this).GetBedElements();
            dbContext = App.GetContext().D_HospitalBedFund.ToList();
        }

        private void Rooms_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button btnBed = sender as Button;
            if (btnBed == null)
            {
                return;

            }

            new RoomInfo().GetRoomInfo(btnBed.Name, out int Room, out int Bed);
            if (dbContext.FirstOrDefault(x => x.Room == Room && x.Bed == Bed && x.PatientId != null) is D_HospitalBedFund bed)
            {
                try
                {
                    DragDrop.DoDragDrop(btnBed, bed, DragDropEffects.Copy);
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void Rooms_PreviewMouseDrop(object sender, DragEventArgs e)
        {
            D_HospitalBedFund place = (D_HospitalBedFund)e.Data.GetData(e.Data.GetFormats()[0]);
            D_Patient patient = place.D_Patient;
            Button btnBed = sender as Button;
            if (btnBed == null)
            {
                return;
            }

            new RoomInfo().GetRoomInfo(btnBed.Name, out int Room, out int Bed);
            //Получаем Bed
            if (dbContext.FirstOrDefault(x => x.Room == Room && x.Bed == Bed && x.PatientId != null) is D_HospitalBedFund bed)
            {
                MessageBox.Show($"Койка-место занято пациентом {bed.D_Patient.LastName} {bed.D_Patient.FirstName}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Устанавливаем значение для прежнего места
            place.D_Patient = null;
            dbContext.FirstOrDefault(x => x.Room == Room && x.Bed == Bed).D_Patient = patient;
            App.GetContext().SaveChanges();
            MessageBox.Show($"Пациент {patient.LastName} {patient.FirstName}" +
                $"был перемещён в палату {Room} на койку {Bed}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            Update();
        }

        private void btnPatientOut(object sender, RoutedEventArgs e)
        {
            if (currentHospitalBedFund.D_Patient != null)
            {
                if (MessageBox.Show($"Выписать пациента {currentHospitalBedFund.D_Patient.LastName} {currentHospitalBedFund.D_Patient.FirstName}?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    currentHospitalBedFund.D_Patient = null;
                    App.GetContext().SaveChanges();
                    Update();
                }
            }

        }

        private void Update()
        {
            var contextBedFund = App.GetContext().D_HospitalBedFund;
            CbPatientPlace.ItemsSource = contextBedFund.Where(x => x.D_Patient == null).ToList();
            CbPatient.SelectedIndex = 0;

            foreach (FrameworkElement btnElement in bedElements)
            {
                new RoomInfo().GetRoomInfo(btnElement.Name, out int room, out int bed);

                Button btnBed = (Button)btnElement;
                if (contextBedFund.FirstOrDefault(x => x.Room == room && x.Bed == bed).D_Patient is D_Patient)
                {
                    btnBed.Content = $"П";
                    btnBed.Background = Brushes.LightGreen;
                }
                else
                {
                    btnBed.Content = null;
                    btnBed.Background = Brushes.Transparent;
                }
            }

            List<D_Patient> dbPatients = App.GetContext().D_Patient.ToList();

            if (!string.IsNullOrWhiteSpace(TbSearchPatient.Text))
            {
                string search = TbSearchPatient.Text.ToLower();
                var currentHospitalPatients = contextBedFund.Where(x => x.PatientId == null).ToList();
                //currentHospitalPatients = currentHospitalPatients.Where(x => x.D_Patient.Medcard.ToLower().Contains(search)).ToList();
                dbPatients = dbPatients.Where(x => x.Medcard.ToLower().Contains(search) && x.D_HospitalBedFund.Count == 0).ToList();
                CbPatient.ItemsSource = dbPatients.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnPatient(object sender, MouseButtonEventArgs e)
        {
            ContextMenu contextMenu = this.FindResource("contextMenu") as ContextMenu;
            if (sender is Button btn && btn.Name.Contains("r_"))
            {
                contextMenu.PlacementTarget = sender as Button;
                contextMenu.IsOpen = true;
                new RoomInfo().GetRoomInfo(btn.Name, out int room, out int bed);
                currentHospitalBedFund = dbContext.FirstOrDefault(x => x.Room == room && x.Bed == bed);
            }
        }

        private void TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void AddPatientInHospital_Click(object sender, RoutedEventArgs e)
        {
            if (CbPatient.SelectedItem is D_Patient patient)
            {
                if (CbPatientPlace.SelectedItem is D_HospitalBedFund place)
                {
                    var patientPlace = dbContext.FirstOrDefault(x => x.Room == place.Room && x.Bed == place.Bed && x.D_Patient == null);
                    patientPlace.PatientId = patient.Id;
                    MessageBox.Show($"Пациент {patient.FIO} был определён {place.Place}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    App.GetContext().SaveChanges();
                    Update();
                }
            }
        }
    }
}
