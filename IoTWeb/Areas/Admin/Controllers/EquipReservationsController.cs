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
    [Authorize(Roles = "user,admin")]
    public class EquipReservationsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/EquipReservations
        public ActionResult Index()
        {
            var eqr = db.EquipReservation.Where(q => q.Review == null).Where(p=>p.ReservationDate>DateTime.Now).OrderBy(q => q.ReservationDate);
            return View(eqr.ToList());
        }
        //-----------------------------------------------------------------------------------
        public ActionResult Index22()
        {
            var eqr = db.EquipReservation.Where(q => q.Review == null).OrderBy(q => q.ReservationDate);
            return View(eqr.ToList());
        }
        public ActionResult GetData()
        {
            var resList = db.EquipReservation.Where(q => q.Review == null).Select(q => new
            {
                q.EquipReservationID,
                q.EquipmentID,
                q.ReservationDate,
                q.ResidentID,
                q.Lessee,
                q.RentTime,
                q.Review
            }).ToList();
            //var aaa = db.EquipReservation.Join(db.Equipment, e => e.EquipmentID, r => r.EquipmentID, (e, r) => 
            //            new {r.EquipmentName,
            //            e.RentTime
            //            });
            var aaa = resList.Join(db.Equipment, e => e.EquipmentID, r => r.EquipmentID, (e, r) =>
                   new {
                       r.EquipmentName,
                       e.EquipReservationID,
                       e.EquipmentID,
                       e.ReservationDate,
                       e.ResidentID,
                       e.Lessee,
                       e.RentTime,
                       e.Review
                   })
                   .Join(db.ResidentDataTable,e=>e.ResidentID,r=>r.ResidentID,(e,r)=>
                   new {
                       e.EquipmentName,
                       e.EquipReservationID,
                       e.EquipmentID,
                       e.ReservationDate,
                       e.ResidentID,
                       e.Lessee,
                       e.RentTime,
                       e.Review,
                       r.ResidentName
                   });
            //return Json(resList, JsonRequestBehavior.AllowGet);
            return Json(aaa, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Addshare(int id = 0)
        {
            if (id == 0)
                return View(new EquipReservation());
            else
            {
                return View(db.EquipReservation.Where(p => p.EquipReservationID == id).FirstOrDefault<EquipReservation>());
            }
        }

        [HttpPost]
        public ActionResult Addshare(EquipReservation eqr)
        {
            if (eqr.EquipReservationID == 0)
            {
                db.EquipReservation.Add(eqr);
                db.SaveChanges();
                return Json(new { success = true, message = "新增錯誤完成！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Entry(eqr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "審核完成！" }, JsonRequestBehavior.AllowGet);
            }
        }

        //-------------------------------------------------------------------------------------------------------
        public ActionResult EqrHistory()
        {
            var eqr = db.EquipReservation.Where(q => q.Review == true).OrderByDescending(q => q.ReservationDate);
            return View(eqr.ToList());
        }

        // GET: Admin/EquipReservations/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/EquipReservations/Edit/5
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
            ViewBag.ReservationDate = db.EquipReservation.Find(id).ReservationDate.ToString("yyyy/MM/dd HH:mm");
            ViewBag.ResidentName = db.ResidentDataTable.Find(db.EquipReservation.Find(id).ResidentID).ResidentName;
            ViewBag.Lessee = db.EquipReservation.Find(id).Lessee;
            ViewBag.RentTime = db.EquipReservation.Find(id).RentTime;


            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
            return View(equipReservation);
        }

        // POST: Admin/EquipReservations/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipReservationID,EquipmentID,ReservationDate,ResidentID,Lessee,RentTime,Review")] EquipReservation equipReservation)
        {
            if (equipReservation.Review == null)
            {
                ModelState.AddModelError("Review", "請選擇是否通過");
            }
            else if (equipReservation.Review == true)
            {

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
                    if (eqL2.ReservationDate < equipReservation.ReservationDate.AddHours(equipReservation.RentTime))
                    {
                        ModelState.AddModelError("ReservationDate", "此時段已經有預約");
                    }
                }
            }

            int id = equipReservation.EquipReservationID;
            ViewBag.EquipmentName = db.Equipment.Find(db.EquipReservation.Find(id).EquipmentID).EquipmentName;
            ViewBag.ReservationDate = db.EquipReservation.Find(id).ReservationDate;
            ViewBag.ResidentName = db.ResidentDataTable.Find(db.EquipReservation.Find(id).ResidentID).ResidentName;
            ViewBag.Lessee = db.EquipReservation.Find(id).Lessee;
            ViewBag.RentTime = db.EquipReservation.Find(id).RentTime;

            if (ModelState.IsValid)
            {
                //db.Entry(equipReservation).State = EntityState.Modified;
                //----------------------------------------------------------
                var a = db.EquipReservation.Where(n => n.EquipReservationID == equipReservation.EquipReservationID).First();
                a.ReservationDate = equipReservation.ReservationDate;
                a.Lessee = equipReservation.Lessee;
                a.RentTime = equipReservation.RentTime;
                a.Review = equipReservation.Review;

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
    }
}
