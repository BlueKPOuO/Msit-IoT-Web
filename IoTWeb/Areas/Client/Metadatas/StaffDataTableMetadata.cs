using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    public class StaffDataTableMetadata
    {
        [Display(Name = "StaffID", ResourceType = typeof(Resource1))]
        public string StaffID { get; set; }
        [Display(Name = "StaffName", ResourceType = typeof(Resource1))]
        public string StaffName { get; set; }
        [Display(Name = "EntryDate", ResourceType = typeof(Resource1))]
        public DateTime EntryDate { get; set; }
        [Display(Name = "LeaveDate", ResourceType = typeof(Resource1))]
        public Nullable<System.DateTime> LeaveDate { get; set; }
        [Display(Name = "StaffPhone", ResourceType = typeof(Resource1))]
        public string StaffPhone { get; set; }
        [Display(Name = "StaffLINE_ID", ResourceType = typeof(Resource1))]
        public string StaffLINE_ID { get; set; }
        [Display(Name = "OnWork", ResourceType = typeof(Resource1))]
        public bool OnWork { get; set; }
        [Display(Name = "ShiftID", ResourceType = typeof(Resource1))]
        public string ShiftID { get; set; }
    }
}