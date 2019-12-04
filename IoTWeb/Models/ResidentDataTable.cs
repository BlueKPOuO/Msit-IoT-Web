//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace IoTWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResidentDataTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResidentDataTable()
        {
            this.HolderDataTable = new HashSet<HolderDataTable>();
            this.ManagementFee = new HashSet<ManagementFee>();
            this.PackageTable = new HashSet<PackageTable>();
            this.ReturnPackage = new HashSet<ReturnPackage>();
            this.PublicSpace = new HashSet<PublicSpace>();
        }
    
        public int ResidentID { get; set; }
        public string ResidentName { get; set; }
        public string ResidentPhone { get; set; }
        public string ResidentIDNumber { get; set; }
        public string CommunityAddress { get; set; }
        public string ResidentLINE_ID { get; set; }
        public bool Living { get; set; }
        public byte[] img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HolderDataTable> HolderDataTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagementFee> ManagementFee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageTable> PackageTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnPackage> ReturnPackage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagementFee> ManagementFee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipReservation> EquipReservation { get; set; }
        public virtual ICollection<PublicSpace> PublicSpace { get; set; }
    }
}
