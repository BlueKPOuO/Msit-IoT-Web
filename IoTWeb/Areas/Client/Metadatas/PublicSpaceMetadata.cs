using System;
using System.ComponentModel.DataAnnotations;
using IoTWeb.Models;
using IoTWeb.Resources;
using System.Collections.Generic;


namespace IoTWeb.Models
{
    public class PublicSpacesMetadata
    {
        
        [Display(Name = "ResidentID", ResourceType = typeof(Resource1))]
        public int ResidentID { get; set; }

        [Display(Name = "管理員ID")]
        public string StaffID { get; set; }

        [Display(Name = "資料ID")]
        public string seq { get; set; }

        [Required]
        [Display(Name = "借用人姓名")]
        public string barrierName { get; set; }

        [Display(Name = "借用地點")]
        public string LocationID { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "請選擇日期")]
        [Display(Name = "借用時間")]
        public System.DateTime StartTime { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "請選擇日期")]
        [Display(Name = "歸還時間")]
        public System.DateTime EndTime { get; set; }

        [Display(Name = "借用事由")]
        public string Reason { get; set; }

        [Display(Name = "登記時間")]
        public Nullable<System.DateTime> DateTimeNow { get; set; }

        [Display(Name = "歸還確認")]
        public Nullable<bool> History { get; set; }


        public virtual Location Location { get; set; }
        public virtual ResidentDataTable ResidentDataTable { get; set; }
        public virtual StaffDataTable StaffDataTable { get; set; }
        }
    }

