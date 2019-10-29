using System;

namespace IoTWeb.Models
{
    internal class BulletinBoardMetadata
    {
        public string StaffID { get; set; }
        public int annID { get; set; }
        public string annGrade { get; set; }
        public string annClass { get; set; }
        public Nullable<System.DateTime> annDate { get; set; }
        public string annTitle { get; set; }
        public string annContent { get; set; }
        public byte[] annAnnex { get; set; }
        public string annFilename { get; set; }

        public virtual StaffDataTable StaffDataTable { get; set; }

    }
}