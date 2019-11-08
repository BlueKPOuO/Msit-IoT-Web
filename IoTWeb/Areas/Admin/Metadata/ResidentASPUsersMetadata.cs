using IoTWeb.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class ResidentASPUsersMetadata
    {
        [Display(Name = "ResidentName", ResourceType = typeof(Resource1))]
        public string ResidentName { get; set; }
        [Display(Name = "CommunityAddress", ResourceType = typeof(Resource1))]
        public string CommunityAddress { get; set; }
        [Display(Name = "AspUserId", ResourceType = typeof(Resource1))]
        public string AspUserId { get; set; }
        [Display(Name = "RoleId", ResourceType = typeof(Resource1))]
        public string RoleId { get; set; }
    }
}