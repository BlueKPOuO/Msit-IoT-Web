using IoTWeb.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class ManagementFeeMetadata
    {
        public string RID { get; set; }
        public int ResidentID { get; set; }
        public int Fee { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:yyyy-MM-dd}")]
        [Display(Name ="PaidDate")]
        public Nullable<System.DateTime> PaidDate { get; set; }

        public virtual ResidentDataTable ResidentDataTable { get; set; }

    }
}