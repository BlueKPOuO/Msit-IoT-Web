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


namespace IoTWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class BulletinBoardsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/BulletinBoards
        public ActionResult Index()
        {

            var bulletinBoard = db.BulletinBoard.Include(b => b.StaffDataTable);
            return View(bulletinBoard.ToList());
        }

        // GET: Admin/BulletinBoards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            if (bulletinBoard == null)
            {
                return HttpNotFound();
            }
            return View(bulletinBoard);
        }

        // GET: Admin/BulletinBoards/Create
        public ActionResult Create()
        {

            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName");
            ViewBag.annGrade1 = new SelectList(db.BulletinBoard, "annGrade");
            return View();

        }

        // POST: Admin/BulletinBoards/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
        {
            if (ModelState.IsValid)
            {
                db.BulletinBoard.Add(bulletinBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
            ViewBag.annGrade1 = new SelectList(db.BulletinBoard, "annGrade", bulletinBoard.annGrade);

            return View(bulletinBoard);
        }

        // GET: Admin/BulletinBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            if (bulletinBoard == null)
            {
                return HttpNotFound();
            }
            //NewMethod(bulletinBoard);
            var GradeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="無", Value="無" },
                new SelectListItem {Text="公告", Value="公告" },
                new SelectListItem {Text="重要公告", Value="重要公告" },
            };
            GradeList.Where(q => q.Value == bulletinBoard.annGrade).First().Selected = true;
            ViewBag.GradeList = GradeList;

            var ClassList = new List<SelectListItem>()
            {
                new SelectListItem {Text="一般公告", Value="一般公告" },
                new SelectListItem {Text="人事公告", Value="人事公告" },
                new SelectListItem {Text="停車公告", Value="停車公告" },
                new SelectListItem {Text="施工公告", Value="施工公告" },
                new SelectListItem {Text="會議通知", Value="會議通知" },
                new SelectListItem {Text="設備更換", Value="設備更換" },
            };
            ClassList.Where(q => q.Value == bulletinBoard.annClass).First().Selected = true;
            ViewBag.ClassList = ClassList;


            return View(bulletinBoard);
        }

        private void NewMethod(BulletinBoard bulletinBoard)
        {
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
        }

        // POST: Admin/BulletinBoards/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bulletinBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
            return View(bulletinBoard);
        }

        // GET: Admin/BulletinBoards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            if (bulletinBoard == null)
            {
                return HttpNotFound();
            }
            return View(bulletinBoard);
        }

        // POST: Admin/BulletinBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            db.BulletinBoard.Remove(bulletinBoard);
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
