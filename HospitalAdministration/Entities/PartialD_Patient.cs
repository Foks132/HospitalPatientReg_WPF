using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAdministration.Entities
{
    partial class D_Patient
    {
        public string FIO
        {
            get
            {
                return $"{this.LastName} {this.FirstName} {this.SecondName}";
            }
        }
    }
}
