using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Areas.Client.Controllers
{
    public class EquipFixesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/EquipFixes/Create
        public ActionResult Create(int id)
        {
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", id);
            string EqName = db.Equipment.Find(id).EquipmentName;
            ViewBag.EqName = EqName;
            return View();
        }

        // POST: Client/EquipFixes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentFixID,EquipmentID,Reason,ReportDate,RepairedDate,Repaired,Real")] EquipFix equipFix)
        {           
            if (equipFix.Reason == null)
            {
                ModelState.AddModelError("Reason", "維修原因不可空白");
            }
            if (ModelState.IsValid)
            {
                db.EquipFix.Add(equipFix);                
                db.SaveChanges();
                return RedirectToAction("Index", "Equipments");
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipFix.EquipmentID);
            string EqName = db.Equipment.Find(equipFix.EquipmentID).EquipmentName;
            ViewBag.EqName = EqName;
            return View(equipFix);
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
