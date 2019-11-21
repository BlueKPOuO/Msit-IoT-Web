using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class PackageTableMetadata
    {
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "PackageArrivalDate", ResourceType = typeof(Resource1))]
        public Nullable<System.DateTime> PackageArrivalDate { get; set; }

        [Display(Name = "Receiver", ResourceType = typeof(Resource1))]
        public string Receiver { get; set; }

        [Required]
        [Display(Name = "Sign", ResourceType = typeof(Resource1))]
        public Nullable<bool> Sign { get; set; }

        [Display(Name = "PackageID", ResourceType = typeof(Resource1))]
        public int PackageID { get; set; }
        [Display(Name = "PackageCompanyID", ResourceType = typeof(Resource1))]
        public string PackageCompanyID { get; set; }

        [Display(Name = "ReceiverID", ResourceType = typeof(Resource1))]
        public Nullable<int> ReceiverID { get; set; }

        [Display(Name = "StaffID", ResourceType = typeof(Resource1))]
        public string StaffID { get; set; }

        public virtual PackageCompany PackageCompany { get; set; }

        public virtual ResidentDataTable ResidentDataTable { get; set; }
        public virtual StaffDataTable StaffDataTable { get; set; }


    }
}