//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalPatitentRegApp.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class D_ReceptionSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public D_ReceptionSchedule()
        {
            this.D_DoctorGroup = new HashSet<D_DoctorGroup>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<bool> isVisited { get; set; }
        public Nullable<int> DoctorGroupId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<D_DoctorGroup> D_DoctorGroup { get; set; }
        public virtual D_Patient D_Patient { get; set; }
    }
}
