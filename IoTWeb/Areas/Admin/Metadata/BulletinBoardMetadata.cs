using System;
using System.ComponentModel.DataAnnotations;

namespace IoTWeb.Models
{
    internal class BulletinBoardMetadata
    {
        internal int No;

        [Display(Name ="發布人員")]
        public string StaffID { get; set; }

        public int annID { get; set; }

        [Display(Name ="公告等級")]
        public string annGrade { get; set; }

        [Display(Name ="公告類別")]
        public string annClass { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy/MM/dd}")]
        [Display(Name ="公告日期")]
        public Nullable<System.DateTime> annDate { get; set; }

        [Display(Name ="公告標題")]
        public string annTitle { get; set; }

        [Display(Name ="公告內容")]
        public string annContent { get; set; }

        [Display(Name ="附件")]
        public byte[] annAnnex { get; set; }

        [Display(Name ="附件名稱")]
        public string annFilename { get; set; }

        public virtual StaffDataTable StaffDataTable { get; set; }

    }
}