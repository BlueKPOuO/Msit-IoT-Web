using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class NoAccountResidentMetadata
    {
        [Display(Name = "ResidentID", ResourceType = typeof(Resource1))]
        public int ResidentID { get; set; }
        [Display(Name = "ResidentName", ResourceType = typeof(Resource1))]
        public string ResidentName { get; set; }
        [Display(Name = "CommunityAddress", ResourceType = typeof(Resource1))]
        public string CommunityAddress { get; set; }
        [Display(Name = "ResidentLINE_ID", ResourceType = typeof(Resource1))]
        public string ResidentLINE_ID { get; set; }
    }
}