using HospitalPatitentRegApp.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalPatitentRegApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MedDbEntities context;
        private static D_User _user;

        public static D_User User { get => _user; set => _user = value; }

        public static MedDbEntities GetContext()
        {
            if (context == null)
            {
                context = new MedDbEntities();
            }
            return context;
        }
    }
}
