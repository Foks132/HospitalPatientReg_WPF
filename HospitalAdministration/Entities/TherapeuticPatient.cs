//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalAdministration.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TherapeuticPatient
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public System.DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public Nullable<int> Type { get; set; }
        public string Result { get; set; }
        public string Name { get; set; }
        public string Recommendations { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
    
        public virtual D_Doctor D_Doctor { get; set; }
        public virtual D_Patient D_Patient { get; set; }
    }
}
