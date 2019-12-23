using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class IoTAlertsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/IoTAlerts
        public async Task<ActionResult> Index()
        {
            return View(await db.IoTAlert.Where(n=>n.Alert==true).ToListAsync());
        }

        // GET: Admin/IoTAlerts/Index2
        public async Task<ActionResult> Index2()
        {
            return View(await db.IoTAlert.ToListAsync());
        }

        // GET: Admin/IoTAlerts/Create
        public ActionResult Create()
        {
            IoTAlert TestAlert = new IoTAlert();
            TestAlert.Alert = true;
            TestAlert.Place = "大廳";
            TestAlert.PS = "測試";
            TestAlert.Time = DateTime.Now;
            db.IoTAlert.Add(TestAlert);
            db.SaveChanges();
            return Redirect(Url.Action("Index", "IoTAlerts", new { Area = "Admin" }));
        }

        // GET: Admin/IoTAlerts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            ioTAlert.Alert = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            //if (ioTAlert == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(ioTAlert);
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
