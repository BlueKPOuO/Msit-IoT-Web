using IoTWeb.Areas.Client.Models;
using IoTWeb.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        [Authorize(Roles = "user,admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Calendar()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var res = db.EquipReservation.Where(r => r.ResidentID == ResidentId).Select(Res => new
            {
                Res.EquipReservationID,
                EquipmentName = Res.Equipment.EquipmentName,
                Res.ReservationDate,
                Res.RentTime
            }).ToList();
            List<CalendarEvents> Cevent = new List<CalendarEvents>();
            foreach (var e in res)
            {
                CalendarEvents c = new CalendarEvents();
                c.id = e.EquipReservationID.ToString();
                c.title = e.EquipmentName;
                c.start = e.ReservationDate;
                c.end = e.ReservationDate.AddHours(e.RentTime);
                c.color = "#36BF36";
                Cevent.Add(c);
            }
            //var Cevents = JsonConvert.DeserializeObject<List<CalendarEvents>>(Cevent);
            return Json(Cevent, JsonRequestBehavior.AllowGet);
        }
    }
}