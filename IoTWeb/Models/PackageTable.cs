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
    
    public partial class PackageTable
    {
        public int PackageID { get; set; }
        public Nullable<System.DateTime> PackageArrivalDate { get; set; }
        public string PackageCompanyID { get; set; }
        public string Receiver { get; set; }
        public Nullable<int> ReceiverID { get; set; }
        public Nullable<bool> Sign { get; set; }
        public string StaffID { get; set; }
        public Nullable<System.DateTime> SignedDate { get; set; }
    
        public virtual PackageCompany PackageCompany { get; set; }
        public virtual ResidentDataTable ResidentDataTable { get; set; }
        public virtual StaffDataTable StaffDataTable { get; set; }
    }
}
