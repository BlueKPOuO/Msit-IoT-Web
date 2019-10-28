using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    public class EquipFixsMetadata
    {
        [Display(Name ="維修單號")]
        public int EquipmentFixID { get; set; }
        [Display(Name = "設備名稱")]
        public int EquipmentID { get; set; }
        [Display(Name = "維修原因")]
        public string Reason { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="報修日期")]
        public System.DateTime ReportDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "維修日期")]
        public Nullable<System.DateTime> RepairedDate { get; set; }
        [Display(Name = "修復完成")]
        public bool Repaired { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}