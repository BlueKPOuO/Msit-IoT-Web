using IoTWeb.Resources;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class IoTAlertMetadata
    {
        [Display(Name = "Place", ResourceType = typeof(Resource1))]
        public string Place { get; set; }
        [Display(Name = "Alert", ResourceType = typeof(Resource1))]
        public bool Alert { get; set; }
        [Display(Name = "PS", ResourceType = typeof(Resource1))]
        public string PS { get; set; }
        public int SN { get; set; }
        [Display(Name = "Time", ResourceType = typeof(Resource1))]
        public System.DateTime Time { get; set; }
    }
}