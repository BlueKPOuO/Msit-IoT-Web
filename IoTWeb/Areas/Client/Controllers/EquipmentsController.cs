using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Areas.Client.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class EquipmentsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/Equipments
        public ActionResult Index(string Plist, string Findeq)
        {        
            var placelst = new List<string>();
            var Pqry = from d in db.Equipment orderby d.Place select d.Place;

            placelst.AddRange(Pqry.Distinct());
            ViewBag.Plist = new SelectList(placelst);

            var pwhere = from m in db.Equipment select m;
            if (!String.IsNullOrEmpty(Findeq))
            {
                pwhere = pwhere.Where(s => s.EquipmentName.Contains(Findeq));
            }
            if (!String.IsNullOrEmpty(Plist))
            {
                pwhere = pwhere.Where(x => x.Place == Plist);
            }
            return View(pwhere);
        }

        public FileResult ShowPhoto(int id)
        {
            byte[] content = db.Equipment.Find(id).Picture;
            return File(content, "image/jpeg");
        }

        // GET: Client/Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
