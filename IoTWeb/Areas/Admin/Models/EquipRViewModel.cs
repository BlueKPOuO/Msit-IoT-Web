using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTWeb.Models
{
    public class EquipRViewModel
    {
        public int EquipReservationID { get; set; }
        public int EquipmentID { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ResidentID { get; set; }
        public string Lessee { get; set; }
        public int RentTime { get; set; }
        public Nullable<bool> Review { get; set; }
        public string ResidentName { get; set; }
        public string EquipmentName { get; set; }
    }
}