using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Controllers
{
    public class EquipFixesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: EquipFixes
        public ActionResult Index()
        {
            var equipFix = db.EquipFix.Include(e => e.Equipment);
            return View(equipFix.ToList());
        }

        // GET: EquipFixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipFix equipFix = db.EquipFix.Find(id);
            if (equipFix == null)
            {
                return HttpNotFound();
            }
            return View(equipFix);
        }

        // GET: EquipFixes/Create
        public ActionResult Create(int id)
        {
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName",id);
            Equipment equipment = db.Equipment.Find(id);
            equipment.Status = "維修中";
            db.SaveChanges();
            return View();
        }

        // POST: EquipFixes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentFixID,EquipmentID,Reason,ReportDate,RepairedDate,Repaired")] EquipFix equipFix)
        {
            if (equipFix.ReportDate>equipFix.RepairedDate)
            {
                ModelState.AddModelError("ReportDate", "維修日期大於報修日期");
            }
            

            if (ModelState.IsValid)
            {
                db.EquipFix.Add(equipFix);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }

        // GET: EquipFixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipFix equipFix = db.EquipFix.Find(id);
            if (equipFix == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }

        // POST: EquipFixes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentFixID,EquipmentID,Reason,ReportDate,RepairedDate,Repaired")] EquipFix equipFix)
        {
            if (equipFix.ReportDate > equipFix.RepairedDate)
            {
                ModelState.AddModelError("ReportDate", "維修日期大於報修日期");
            }
            if (ModelState.IsValid)
            {
                db.Entry(equipFix).State = EntityState.Modified;
                Equipment equipment = db.Equipment.Find(equipFix.EquipmentID);
                if (equipFix.Repaired==true)
                {
                    equipment.Status = "正常";
                }
                else
                {
                    equipment.Status = "維修中";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }

        // GET: EquipFixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipFix equipFix = db.EquipFix.Find(id);
            if (equipFix == null)
            {
                return HttpNotFound();
            }
            return View(equipFix);
        }

        // POST: EquipFixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipFix equipFix = db.EquipFix.Find(id);
            db.EquipFix.Remove(equipFix);
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
