using HospitalPatitentRegApp.Entities;
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

namespace HospitalPatitentRegApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = App.GetContext().D_User.
                FirstOrDefault(x => x.Login == TbLogin.Text &&
                x.Password == PbPassword.Password);
            if (user != null)
            {
                App.User = user;
                MessageBox.Show($"Вы успешно авторизировались.\nУчетная запись: {user.Email}", "Авторизация", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                TbLogin.Text = null;
                var test = Application.Current.MainWindow;
                test.UpdateLayout();
                
                NavigationService.Navigate(new HospitalRoomsPage());
            }
        }
    }
}
