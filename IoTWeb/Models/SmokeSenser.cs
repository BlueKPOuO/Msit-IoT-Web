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
    
    public partial class SmokeSenser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SmokeSenser()
        {
            this.SmokeSenserData = new HashSet<SmokeSenserData>();
        }
    
        public string SensorID { get; set; }
        public string place { get; set; }
        public string Vendor { get; set; }
        public string Status { get; set; }
        public Nullable<double> Frequency { get; set; }
        public string CategoryID { get; set; }
    
        public virtual SensorTable SensorTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SmokeSenserData> SmokeSenserData { get; set; }
    }
}
