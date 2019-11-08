using IoTWeb.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class ManagementFeeMetadata
    {
        public int RID { get; set; }
        public int ResidentID { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="請輸入大於{1}之數值")]
        [Display(Name ="管理費用")]
        public int Fee { get; set; }


        [Display(Name ="年度")]
        public int Year { get; set; }

        [Range(1,12,ErrorMessage ="請填入{1}至{2}範圍內的月份")]
        [Display(Name ="月份")]
        public int Month { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:yyyy-MM-dd}")]
        [Display(Name ="繳費日期")]
        public Nullable<System.DateTime> PaidDate { get; set; }

        public virtual ResidentDataTable ResidentDataTable { get; set; }

    }
}