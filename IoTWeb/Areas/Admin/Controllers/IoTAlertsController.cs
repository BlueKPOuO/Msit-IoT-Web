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
    public class IoTAlertsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/IoTAlerts
        public async Task<ActionResult> Index()
        {
            return View(await db.IoTAlert.Where(n=>n.Alert==true).ToListAsync());
        }

        public async Task<ActionResult> Index2()
        {
            return View(await db.IoTAlert.ToListAsync());
        }

        // GET: Admin/IoTAlerts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            if (ioTAlert == null)
            {
                return HttpNotFound();
            }
            return View(ioTAlert);
        }

        // GET: Admin/IoTAlerts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/IoTAlerts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Place,Alert,PS,SN,Time")] IoTAlert ioTAlert)
        {
            if (ModelState.IsValid)
            {
                db.IoTAlert.Add(ioTAlert);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ioTAlert);
        }

        // GET: Admin/IoTAlerts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            if (ioTAlert == null)
            {
                return HttpNotFound();
            }
            return View(ioTAlert);
        }

        // POST: Admin/IoTAlerts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Place,Alert,PS,SN,Time")] IoTAlert ioTAlert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ioTAlert).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ioTAlert);
        }

        // GET: Admin/IoTAlerts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            if (ioTAlert == null)
            {
                return HttpNotFound();
            }
            return View(ioTAlert);
        }

        // POST: Admin/IoTAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IoTAlert ioTAlert = await db.IoTAlert.FindAsync(id);
            db.IoTAlert.Remove(ioTAlert);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
