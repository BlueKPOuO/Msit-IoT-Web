using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IoTWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    class LocationMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocationMetadata()
        {
            this.History = new HashSet<History>();
            this.PublicSpace = new HashSet<PublicSpace>();
        }

         private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        [Display(Name = "場地ID")]
        public string LocationID { get; set; }
       
        [Display(Name = "場地名稱")]
        public string Location1 { get; set; }
        public Nullable<bool> 是否出借 { get; set; }
        [Display(Name = "上傳圖片")]
        public byte[] Photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> History { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PublicSpace> PublicSpace { get; set; }
    }
}