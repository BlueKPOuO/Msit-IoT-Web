using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class ReturnPackageMetadata
    {
        [Display(Name = "ReturnDataID", ResourceType = typeof(Resource1))]
        public int ReturnDataID { get; set; }

        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "ReturnArrivalDate", ResourceType = typeof(Resource1))]
        public Nullable<System.DateTime> ReturnArrivalDate { get; set; }

        [Display(Name = "ReturnCompanyID", ResourceType = typeof(Resource1))]
        public string ReturnCompanyID { get; set; }

        [Display(Name = "Returnee", ResourceType = typeof(Resource1))]
        public string Returnee { get; set; }
        [Display(Name = "ReturneeID", ResourceType = typeof(Resource1))]
        public Nullable<int> ReturneeID { get; set; }
        [Display(Name = "Sign", ResourceType = typeof(Resource1))]
        public Nullable<bool> Sign { get; set; }
        [Display(Name = "StaffID", ResourceType = typeof(Resource1))]
        public string StaffID { get; set; }

        public virtual PackageCompany PackageCompany { get; set; }
        public virtual ResidentDataTable ResidentDataTable { get; set; }
    }
}