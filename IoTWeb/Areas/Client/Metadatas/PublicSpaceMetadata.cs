using System;
using System.ComponentModel.DataAnnotations;
using IoTWeb.Models;
using IoTWeb.Resources;

namespace IoTWeb.Areas.Client.Metadatas
{
    public class PublicSpaceMetadata
    {       
        [Display(Name ="住戶ID", ResourceType = typeof(Resource1))]
        public int ResidentID { get; set; }

        [Display(Name = "管理員ID", ResourceType = typeof(Resource1))]
        public string StaffID { get; set; }

        [Display(Name = "資料ID", ResourceType = typeof(Resource1))]
        public string seq { get; set; }

        [Display(Name = "借用人姓名", ResourceType = typeof(Resource1))]
        public string barrierName { get; set; }

        [Display(Name = "借用地點", ResourceType = typeof(Resource1))]
        public string LocationID { get; set; }

        [Display(Name = "借用時間", ResourceType = typeof(Resource1))]
        public System.DateTime StartTime { get; set; }

        [Display(Name = "歸還時間", ResourceType = typeof(Resource1))]
        public System.DateTime EndTime { get; set; }

        [Display(Name = "借用事由", ResourceType = typeof(Resource1))]
        public string Reason { get; set; }

        [Display(Name = "登記時間", ResourceType = typeof(Resource1))]
        public Nullable<System.DateTime> DateTimeNow
        {
            get
            {

                return DateTime.Now;
            }
            set
            {
                string s = DateTime.Now.ToString();
                
            }
        }

        public virtual Location Location { get; set; }
        public virtual ResidentDataTable ResidentDataTable { get; set; }
        public virtual StaffDataTable StaffDataTable { get; set; }
        }
    }

