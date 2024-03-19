using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAdministration.Entities
{
    partial class D_Doctor
    {
        public string FIO
        {
            get
            {
                return $"{this.LastName} {this.FirstName} {this.SecondName}";
            }
            set
            {
                string[] fio = FIO.Split(' ');
                this.LastName = fio[0];
                this.FirstName = fio[1];
                this.SecondName = fio[2];
            }
        }
        public string DoctorWithCategory
        {
            get
            {
                return $"{this.FIO} | {this.D_Category.Name}";
            }
        }
    }
}
