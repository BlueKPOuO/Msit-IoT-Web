﻿using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class PackageTableMetadata
    {
        [Display(Name = "PackageArrivalDate", ResourceType = typeof(Resource1))]
        public Nullable<System.DateTime> PackageArrivalDate { get; set; }
        [Display(Name = "Receiver", ResourceType = typeof(Resource1))]
        public string Receiver { get; set; }
        [Display(Name = "Sign", ResourceType = typeof(Resource1))]
        public Nullable<bool> Sign { get; set; }

        public string StaffID { get; set; }

        public virtual PackageCompany PackageCompany { get; set; }

        public virtual ResidentDataTable ResidentDataTable { get; set; }
        public virtual StaffDataTable StaffDataTable { get; set; }
    }
}