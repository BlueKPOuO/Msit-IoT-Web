using IoTWeb.Resources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class ResidentDataTableMetadata
    {
        [Display(Name = "ResidentID", ResourceType = typeof(Resource1))]
        public int ResidentID { get; set; }
        [Display(Name = "ResidentName", ResourceType = typeof(Resource1))]
        public string ResidentName { get; set; }
        [Display(Name = "ResidentPhone", ResourceType = typeof(Resource1))]
        public string ResidentPhone { get; set; }
        [Display(Name = "ResidentIDNumber", ResourceType = typeof(Resource1))]
        public string ResidentIDNumber { get; set; }
        [Display(Name = "CommunityAddress", ResourceType = typeof(Resource1))]
        public string CommunityAddress { get; set; }
        [Display(Name = "ResidentLINE_ID", ResourceType = typeof(Resource1))]
        public string ResidentLINE_ID { get; set; }
        [Display(Name = "Living", ResourceType = typeof(Resource1))]
        public bool Living { get; set; }
    }
}