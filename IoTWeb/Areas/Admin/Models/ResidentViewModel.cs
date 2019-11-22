namespace IoTWeb.Areas.Admin.Controllers
{
    internal class ResidentViewModel
    {
        public int ResidentID { get; set; }
        public string ResidentName { get; set; }
        public string ResidentPhone { get; set; }
        public string ResidentIDNumber { get; set; }
        public string CommunityAddress { get; set; }
        public string ResidentLINE_ID { get; set; }
        public bool Living { get; set; }
        public byte[] img { get; set; }
    }
}