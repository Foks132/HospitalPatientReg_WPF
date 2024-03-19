using HospitalAdministration.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalAdministration
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MedDbEntities context;
        public static MedDbEntities GetContext()
        {
            if (context == null)
            {
                context = new MedDbEntities();
            }
            return context;
        }

        private static D_User user;

        public static D_User User { get => user; set => user = value; }
    }
}
