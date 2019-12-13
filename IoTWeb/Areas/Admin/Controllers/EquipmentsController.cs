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


namespace IoTWeb.Areas.Admin.Controllers
{
    public class EquipmentsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/Equipments
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
                pwhere = pwhere.Where(x=>x.Place== Plist);
            }
            return View(pwhere);
        }

        public FileResult ShowPhoto(int id)
        {
            byte[] content = db.Equipment.Find(id).Picture;
            return File(content, "image/jpeg");
        }

        // GET: Admin/Equipments/Details/5
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

        // GET: Admin/Equipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Equipments/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentID,EquipmentName,Place,Vendor,Status,Buydate,UseYear,Picture")] Equipment equipment)
        {
            if (equipment.EquipmentName == null)
            {
                ModelState.AddModelError("EquipmentName", "設備名稱 欄位是必要項。");
            }
            if (equipment.Place == null)
            {
                ModelState.AddModelError("Place", "設置地點 欄位是必要項。");
            }
            if (equipment.Status == null)
            {
                ModelState.AddModelError("Status", "狀態 欄位是必要項。");
            }
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)
                {
                    byte[] data = null;
                    using (BinaryReader br = new BinaryReader(Request.Files["File1"].InputStream))
                    {
                        data = br.ReadBytes(Request.Files["File1"].ContentLength);
                    }
                    equipment.Picture = data;
                }
                db.Equipment.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipment);
        }

        // GET: Admin/Equipments/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/Equipments/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentID,EquipmentName,Place,Vendor,Status,Buydate,UseYear,Picture")] Equipment equipment)
        {         
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)
                {
                    byte[] data = null;
                    using (BinaryReader br = new BinaryReader(Request.Files["File1"].InputStream))
                    {
                        data = br.ReadBytes(Request.Files["File1"].ContentLength);
                    }
                    equipment.Picture = data;
                }
                else
                {
                    Equipment c = db.Equipment.Find(equipment.EquipmentID);
                    c.EquipmentName = equipment.EquipmentName;                    
                    equipment = c;
                }
                db.Entry(equipment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Admin/Equipments/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipment.Find(id);
            db.Equipment.Remove(equipment);
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
