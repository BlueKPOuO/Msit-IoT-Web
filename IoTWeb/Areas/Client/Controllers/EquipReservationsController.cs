using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Areas.Client.Models;
using IoTWeb.Models;
using Microsoft.AspNet.Identity;

namespace IoTWeb.Areas.Client.Controllers
{
    public class EquipReservationsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/EquipReservations
        public ActionResult Index()
        {
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var dview = db.EquipReservation.AsEnumerable().Where(e => e.ResidentID == Residentid)
                .Where(d => d.ReservationDate > DateTime.Now).Where(q=>q.Review==false);
            return View(dview.ToList());
        }

        public ActionResult Indexok()
        {
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var dview = db.EquipReservation.AsEnumerable().Where(e => e.ResidentID == Residentid)
                .Where(d => d.ReservationDate > DateTime.Now).Where(q => q.Review == true);
            return View(dview.ToList());
        }

        public ActionResult Indexhistory()
        {
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var dview = db.EquipReservation
                        .Where(e => e.ResidentID == Residentid)
                        .Where(d => d.ReservationDate < DateTime.Now)
                        .Where(p=>p.Review==true)
                        .OrderBy(d=>d.ReservationDate);
            return View(dview.ToList());
        }
        //TODO--------------------------------------------------
        public ActionResult Favorite()
        {
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var eview = db.EquipReservation.AsEnumerable().Where(q => q.ResidentID == Residentid);
            
            ViewBag.aa = eview;
            //return View();
            return View(eview.ToList());
        }

        // GET: Client/EquipReservations/Create
        public ActionResult Create(int id)
        {
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            string ResidentID = db.ResidentDataTable.Find(Residentid).ResidentName;
            
            string EqName = db.Equipment.Find(id).EquipmentName;
            //----------------------------------------------------
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", id);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", Residentid);
            ViewBag.ResidentIDname = ResidentID;
            ViewBag.EqName = EqName;


            return View();
        }

        // POST: Client/EquipReservations/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipReservationID,EquipmentID,ReservationDate,ResidentID,Lessee,RentTime,Review")] EquipReservation equipReservation)
        {           
            if (equipReservation.ReservationDate < DateTime.Now)
            {
                ModelState.AddModelError("ReservationDate", "預約日期小於現在");
            }
            //前一筆
            var eqL = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
                 Where(e => e.ReservationDate <= equipReservation.ReservationDate).Where(q=>q.Review==true).LastOrDefault();           
            if (eqL!=null)
            {
                if (eqL.ReservationDate.AddHours(eqL.RentTime) > equipReservation.ReservationDate)
                {
                    ModelState.AddModelError("ReservationDate", "此時段已經有預約");                    
                }           
            }
            //後一筆
            var eqL2 = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
                Where(e => e.ReservationDate >= equipReservation.ReservationDate).Where(q => q.Review == true).FirstOrDefault();
            if (eqL2 != null)
            {
                if (eqL2.ReservationDate < equipReservation.ReservationDate)
                {
                    ModelState.AddModelError("ReservationDate", "此時段已經有預約");
                }
            }

            if (ModelState.IsValid)
            {
                db.EquipReservation.Add(equipReservation);
                Equipment equipment = db.Equipment.Find(equipReservation.EquipmentID);
                if (equipment.Status != "正常")
                {
                    ModelState.AddModelError("EquipmentID", "狀態中不可預約");
                }   
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //失敗 再傳送設備名稱跟戶長名稱
            string NowUser = User.Identity.GetUserName();
            int Residentid = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            string ResidentID = db.ResidentDataTable.Find(Residentid).ResidentName;
            string EqName = db.Equipment.Find(equipReservation.EquipmentID).EquipmentName;
            //----------------------------------------------------
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", Residentid);
            ViewBag.ResidentIDname = ResidentID;
            ViewBag.EqName = EqName;
            return View(equipReservation);
        }

        // GET: Client/EquipReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipReservation equipReservation = db.EquipReservation.Find(id);
            if (equipReservation == null)
            {
                return HttpNotFound();
            }
            return View(equipReservation);
        }

        // POST: Client/EquipReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipReservation equipReservation = db.EquipReservation.Find(id);
            db.EquipReservation.Remove(equipReservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: Client/EquipReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipReservation equipReservation = db.EquipReservation.Find(id);
            if (equipReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipReservation.Find(id).EquipmentID).EquipmentName;            
            ViewBag.ResidentName = db.ResidentDataTable.Find(db.EquipReservation.Find(id).ResidentID).ResidentName;           
            ViewBag.RentTime = db.EquipReservation.Find(id).RentTime;

            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
            return View(equipReservation);
        }

        // POST: Client/EquipReservations/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipReservationID,EquipmentID,ReservationDate,ResidentID,Lessee,RentTime,Review")] EquipReservation equipReservation)
        {
            if (equipReservation.ReservationDate < DateTime.Now)
            {
                ModelState.AddModelError("ReservationDate", "預約日期小於現在");
            }
            var eqL = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
                 Where(e => e.ReservationDate <= equipReservation.ReservationDate).Where(q => q.Review == true).LastOrDefault();
            if (eqL != null)
            {
                if (eqL.ReservationDate.AddHours(eqL.RentTime) > equipReservation.ReservationDate)
                {
                    ModelState.AddModelError("ReservationDate", "此時段已經有預約");
                }
            }
            var eqL2 = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
              Where(e => e.ReservationDate >= equipReservation.ReservationDate).Where(q => q.Review == true).FirstOrDefault();
            if (eqL2 != null)
            {
                if (eqL2.ReservationDate > equipReservation.ReservationDate)
                {
                    ModelState.AddModelError("ReservationDate", "此時段已經有預約");
                }
            }
            int id = equipReservation.EquipReservationID;
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipReservation.Find(id).EquipmentID).EquipmentName;           
            ViewBag.ResidentName = db.ResidentDataTable.Find(db.EquipReservation.Find(id).ResidentID).ResidentName;           
            ViewBag.RentTime = db.EquipReservation.Find(id).RentTime;

            if (ModelState.IsValid)
            {
                //db.Entry(equipReservation).State = System.Data.Entity.EntityState.Modified;
                var a = db.EquipReservation.Where(n => n.EquipReservationID == equipReservation.EquipReservationID).First();
                a.Review = equipReservation.Review;
                a.RentTime = equipReservation.RentTime;
                a.Lessee = equipReservation.Lessee;
                a.ReservationDate = equipReservation.ReservationDate;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
            return View(equipReservation);
        }


        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Eqrlist(int id)
        {
            ViewBag.id = id;
            ViewBag.Name = db.Equipment.Find(id).EquipmentName;
            return View();
        }

        public JsonResult Calendar(int id)
        {          
            var res = db.EquipReservation.AsEnumerable().Where(p => p.EquipmentID == id).Where(p=>p.Review==true).Select(Res => new
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
                //c.title = e.EquipmentName;
                c.start = e.ReservationDate;
                c.end = e.ReservationDate.AddHours(e.RentTime);
                c.color = "#36BF36";
                Cevent.Add(c);
            }

            return Json(Cevent, JsonRequestBehavior.AllowGet);
        }        

    }
}
