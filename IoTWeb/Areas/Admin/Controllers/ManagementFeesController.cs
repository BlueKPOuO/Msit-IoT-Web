using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Areas.Admin.Controllers
{
    public class ManagementFeesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/ManagementFees
        public ActionResult Index()
        {
            var managementFee = db.ManagementFee.Include(m => m.ResidentDataTable);
            return View(managementFee.ToList());
        }

        // GET: Admin/ManagementFees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementFee managementFee = db.ManagementFee.Find(id);
            if (managementFee == null)
            {
                return HttpNotFound();
            }
            return View(managementFee);
        }

//        // POST: Admin/ManagementFees/Create
//        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
//        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,ResidentID,ManagementFee1,Year,Month")] ManagementFee managementFee)
//        {
//            if (ModelState.IsValid)
//            {
//                db.ManagementFee.Add(managementFee);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

        // POST: Admin/ManagementFees/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RID,ResidentID,Fee,Year,Month,PaidDate")] ManagementFee managementFee)
        {
            if (ModelState.IsValid)
            {
                db.ManagementFee.Add(managementFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", managementFee.ResidentID);
            return View(managementFee);
        }

        // GET: Admin/ManagementFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementFee managementFee = db.ManagementFee.Find(id);
            if (managementFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", managementFee.ResidentID);
            return View(managementFee);
        }

        // POST: Admin/ManagementFees/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RID,ResidentID,Fee,Year,Month,PaidDate")] ManagementFee managementFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managementFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", managementFee.ResidentID);
            return View(managementFee);
        }

        // GET: Admin/ManagementFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagementFee managementFee = db.ManagementFee.Find(id);
            if (managementFee == null)
            {
                return HttpNotFound();
            }
            return View(managementFee);
        }

        // POST: Admin/ManagementFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ManagementFee managementFee = db.ManagementFee.Find(id);
            db.ManagementFee.Remove(managementFee);
            db.SaveChanges();
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
