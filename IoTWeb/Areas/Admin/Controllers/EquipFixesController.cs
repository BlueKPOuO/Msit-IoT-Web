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
    [Authorize(Roles = "admin")]
    public class EquipFixesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/EquipFixes
        public ActionResult Index()
        {            
            var eqf = db.EquipFix.Where(p => p.Real == false).OrderBy(p => p.ReportDate);
            return View(eqf.ToList());
        }

        public ActionResult EqfHistory()
        {
            var eqf = db.EquipFix.Where(p => p.Repaired == true).OrderByDescending(p => p.ReportDate);
            return View(eqf.ToList());
        }

        public ActionResult IndexReal()
        {
            var eqf = db.EquipFix.Where(p => p.Real == true).Where(p=>p.Repaired!=true).OrderBy(p => p.ReportDate);
            return View(eqf.ToList());
        }

        // GET: Admin/EquipFixes/Details/5
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

        // GET: Admin/EquipFixes/Edit/5
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
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipFix.Find(id).EquipmentID).EquipmentName;
            ViewBag.ReportDate = db.EquipFix.Find(id).ReportDate.ToString("yyyy/MM/dd");

            //ViewBag.RepairedDate= db.EquipFix.Find(id).RepairedDate;
            ViewBag.Reason= db.EquipFix.Find(id).Reason;
            if (db.EquipFix.Find(id).Repaired==true)
            {
                ViewBag.Eqf = "ind";
                ViewBag.Title = "維修完成確認";
            }
            else
            {
                ViewBag.Eqf = "his";
                ViewBag.Title = "維修歷史資料";
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }

        // POST: Admin/EquipFixes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentFixID,EquipmentID,Reason,ReportDate,RepairedDate,Repaired,Real")] EquipFix equipFix)
        {
            if (equipFix.ReportDate > equipFix.RepairedDate)
            {
                ModelState.AddModelError("RepairedDate", "維修日期大於報修日期");
            }            
            if (equipFix.RepairedDate==null)
            {
                ModelState.AddModelError("RepairedDate", "維修日期不可空白");
            }
            if (ModelState.IsValid)
            {
                //---------------------------------------------------

                Equipment equipment = db.Equipment.Find(equipFix.EquipmentID);
                equipment.Status = "正常";

                db.Entry(equipFix).State = System.Data.Entity.EntityState.Modified;              
                equipFix.Real = true;
                equipFix.Repaired = true;                
                db.SaveChanges();
                return RedirectToAction("EqfHistory");
            }
            int id = equipFix.EquipmentFixID;
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipFix.Find(id).EquipmentID).EquipmentName;
            ViewBag.ReportDate = db.EquipFix.Find(id).ReportDate.ToString("yyyy/MM/dd");
            ViewBag.Reason = db.EquipFix.Find(id).Reason;
       
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }


        // GET: Admin/EquipFixes/Edit/5
        public ActionResult EditReal(int? id)
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
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipFix.Find(id).EquipmentID).EquipmentName;
            ViewBag.ReportDate = db.EquipFix.Find(id).ReportDate.ToString("yyyy/MM/dd");
            ViewBag.Reason = db.EquipFix.Find(id).Reason;
            ViewBag.Title = "確認真的壞掉";
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }

        // POST: Admin/EquipFixes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReal([Bind(Include = "EquipmentFixID,EquipmentID,Reason,ReportDate,RepairedDate,Repaired,Real")] EquipFix equipFix)
        {
            if (equipFix.Real==false)
            {
                ModelState.AddModelError("Real", "請勾選");
            }
            if (ModelState.IsValid)
            {                
                db.Entry(equipFix).State = System.Data.Entity.EntityState.Modified;

                Equipment equipment = db.Equipment.Find(equipFix.EquipmentID);
                equipment.Status = "維修中";

                db.SaveChanges();
                return RedirectToAction("IndexReal");
            }
            int id = equipFix.EquipmentFixID;
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipFix.Find(id).EquipmentID).EquipmentName;
            ViewBag.ReportDate = db.EquipFix.Find(id).ReportDate.ToString("yyyy/MM/dd");
            ViewBag.Reason = db.EquipFix.Find(id).Reason;
            ViewBag.Title = "確認真的壞掉";

            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            return View(equipFix);
        }




        // GET: Admin/EquipFixes/Delete/5
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

        // POST: Admin/EquipFixes/Delete/5
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
