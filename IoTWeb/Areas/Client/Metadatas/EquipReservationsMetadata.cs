using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    public class EquipReservationsMetadata
    {
        [Display(Name = "預約編號")]
        public int EquipReservationID { get; set; }
        [Display(Name = "設備名稱")]
        public int EquipmentID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "預約日期")]
        public System.DateTime ReservationDate { get; set; }
        [Display(Name = "住戶名稱")]
        public int ResidentID { get; set; }
        [Display(Name ="預約者")]
        public string Lessee { get; set; }
        [Display(Name = "預約時數")]
        public int RentTime { get; set; }
        [Display(Name = "審核情形")]
        public bool Review { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual ResidentDataTable ResidentDataTable { get; set; }
    }
}