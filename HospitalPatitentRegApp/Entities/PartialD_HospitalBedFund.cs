using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPatitentRegApp.Entities
{
    partial class D_HospitalBedFund
    {
        public string Place
        {
            get
            {
                return $"Палата: {this.Room} Место: {this.Bed}";
            }
        }
    }
}
