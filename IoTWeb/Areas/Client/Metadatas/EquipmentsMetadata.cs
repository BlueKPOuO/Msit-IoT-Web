using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    public class EquipmentsMetadata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentsMetadata()
        {
            this.EquipFix = new HashSet<EquipFix>();
            this.EquipReservation = new HashSet<EquipReservation>();
        }

        [Display(Name = "設備編號")]
        public int EquipmentID { get; set; }
        [Display(Name = "設備名稱")]
        public string EquipmentName { get; set; }
        [Display(Name = "設置地點")]
        public string Place { get; set; }
        [Display(Name = "廠商")]
        public string Vendor { get; set; }
        [Display(Name = "狀態")]
        public string Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "購買日期")]
        public System.DateTime Buydate { get; set; }
        [Display(Name = "使用年限")]
        public int UseYear { get; set; }
        [Display(Name = "圖片")]
        public byte[] Picture { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipFix> EquipFix { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipReservation> EquipReservation { get; set; }
    }


}