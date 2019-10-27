using IoTWeb.Resources;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class PackageCompanyMetadata
    {
        [Display(Name = "PackageCompanyID", ResourceType = typeof(Resource1))]
        public string PackageCompanyID { get; set; }
        [Display(Name = "CompanyName", ResourceType = typeof(Resource1))]
        public string CompanyName { get; set; }
    }
}