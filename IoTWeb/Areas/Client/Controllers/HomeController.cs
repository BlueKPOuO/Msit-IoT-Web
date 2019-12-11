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
    [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Registered-Error")]
    public class HomeController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        [Authorize(Roles = "user,admin")]
        [HandleError(ExceptionType = typeof(InvalidOperationException),View = "Registered-Error")]
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

        public ActionResult MostUsedEquip()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var MUE = from c in db.EquipReservation
                      where c.ResidentID == ResidentId && c.ReservationDate < DateTime.Now
                      group c by new { c.EquipmentID, c.Equipment.EquipmentName} into g
                      orderby g.Count() descending
                      select new { Count = g.Count(),g.Key.EquipmentID, g.Key.EquipmentName, TotalTime = g.Sum(t => t.RentTime) };

            return Json(MUE, JsonRequestBehavior.AllowGet);
        }
    }
}