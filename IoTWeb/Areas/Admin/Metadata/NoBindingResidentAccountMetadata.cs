using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class NoBindingResidentAccountMetadata
    {
        [Display(Name = "UserName", ResourceType = typeof(Resource1))]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource1))]
        public string Email { get; set; }

        [Display(Name = "RoleId", ResourceType = typeof(Resource1))]
        public string RoleId { get; set; }
    }
}