using HospitalAdministration.MedDbDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAdministration.Entities
{
    partial class D_ReceptionSchedule
    {
        public string DoctorsReception
        {
            get
            {
                StringBuilder sb = new StringBuilder();


                var doctors = App.GetContext().D_DoctorGroup.Where(x => x.Id == this.DoctorGroupId);
                foreach (var doctor in doctors)
                {
                    sb.AppendLine(doctor.D_Doctor.FIO);
                }
                return sb.ToString();
            }
        }

        public string CategoryReception
        {
            get
            {
                var chiefDoctor = App.GetContext().D_DoctorGroup.FirstOrDefault(x=>x.Id == this.DoctorGroupId && x.isDoctorChief==true);
                if (chiefDoctor == null)
                {
                    return "";
                }
                return chiefDoctor.D_Doctor.D_Category.Name;
            }
        }

        public string isVisitedColor
        {
            get
            {
                if (this.isVisited == null)
                {
                    return "#33C71585";
                }
                if (this.isVisited == true)
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
