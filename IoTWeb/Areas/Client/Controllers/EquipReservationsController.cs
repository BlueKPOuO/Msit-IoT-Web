﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            //string NowUser = User.Identity.GetUserName();
            //int Residentid = db.ResidentASPUsers.Where(n => n.ResidentName == NowUser).Select(n => n.).First();
            //----------------------------------------------------
            var equipReservation = db.EquipReservation.Include(e => e.Equipment).Include(e => e.ResidentDataTable);
            return View(equipReservation.ToList());
        }

        // GET: Client/EquipReservations/Details/5
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

        // GET: Client/EquipReservations/Create
        public ActionResult Create(int id)
        {
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", id);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName");
            return View();
        }

        // POST: Client/EquipReservations/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipReservationID,EquipmentID,ReservationDate,ResidentID,ReturnDate")] EquipReservation equipReservation)
        {
            //if (equipReservation.ReservationDate > equipReservation.ReturnDate)
            //{
            //    ModelState.AddModelError("ReservationDate", "預約日期大於歸還日期");
            //}
            //-------------------------------------------------
            //這筆預約的 equipReservation.ReservationDate;
            //var eqL = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
            //     Where(e => e.ReservationDate <= equipReservation.ReservationDate).LastOrDefault();
            var eqL = db.EquipReservation.AsEnumerable().Where(e => e.EquipmentID == equipReservation.EquipmentID).
                 Where(e => e.ReservationDate <= equipReservation.ReservationDate).Where(q=>q.Review==true).LastOrDefault();



            if (equipReservation.ReservationDate < DateTime.Now)
            {
                ModelState.AddModelError("ReservationDate", "預約日期小於現在");
                
            }
            if (eqL!=null)
            {
                if (eqL.ReservationDate.AddHours(eqL.RentTime) > equipReservation.ReservationDate)
                {
                    ModelState.AddModelError("ReservationDate", "此時段已經有預約");                    
                }
           
            }
            
            //equipReservation.ReservationDate.AddHours(equipReservation.RentTime);
            //---------------------------------------------------
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

            //ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            //ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
            return View(equipReservation);
        }

        // GET: Client/EquipReservations/Edit/5
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
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
            return View(equipReservation);
        }

        // POST: Client/EquipReservations/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipReservationID,EquipmentID,ReservationDate,ResidentID,ReturnDate")] EquipReservation equipReservation)
        {
            /*if (equipReservation.ReservationDate > equipReservation.ReturnDate)
            {
                ModelState.AddModelError("ReservationDate", "預約日期大於歸還日期");
            }*/
            if (ModelState.IsValid)
            {
                db.Entry(equipReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "EquipmentName", equipReservation.EquipmentID);
            ViewBag.ResidentID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", equipReservation.ResidentID);
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
