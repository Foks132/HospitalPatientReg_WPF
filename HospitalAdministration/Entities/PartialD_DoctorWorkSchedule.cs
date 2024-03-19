using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAdministration.Entities
{
    partial class D_DoctorWorkSchedule
    {
        public string isWorkedColor
        {
            get
            {
                if (this.isWorked == null)
                {
                    return "#33C71585";
                }
                if (this.isWorked == true) 
                {
                    return "#3390EE90";
                }
                else
                {
                    return "#33FFDEAD";
                }
            }
        }
    }
}
