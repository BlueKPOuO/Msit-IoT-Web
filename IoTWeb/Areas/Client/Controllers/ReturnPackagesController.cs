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

namespace IoTWeb.Areas.Client.Metadatas
{
    public class ReturnPackagesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/ReturnPackages
        public ActionResult Index()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var returnPackage = db.ReturnPackage.Include(r => r.PackageCompany).Include(r => r.ResidentDataTable).Where(r => r.Sign == false && r.ReturneeID == ResidentId);
            return View(returnPackage.ToList());
        }
        public ActionResult Index2()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var returnPackage = db.ReturnPackage.Include(r => r.PackageCompany).Include(r => r.ResidentDataTable).Where(r => r.Sign == true && r.ReturneeID == ResidentId);
            return View(returnPackage.ToList());
        }
        // GET: Client/ReturnPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnPackage returnPackage = db.ReturnPackage.Find(id);
            if (returnPackage == null)
            {
                return HttpNotFound();
            }
            return View(returnPackage);
        }

        // GET: Client/ReturnPackages/Create
        public ActionResult Create()
        {
            ViewBag.ReturnCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName");
            ViewBag.ReturneeID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName");
            return View();
        }

        // POST: Client/ReturnPackages/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReturnDataID,ReturnArrivalDate,ReturnCompanyID,Returnee,ReturneeID,Sign,StaffID")] ReturnPackage returnPackage)
        {
            if (ModelState.IsValid)
            {
                db.ReturnPackage.Add(returnPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReturnCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", returnPackage.ReturnCompanyID);
            ViewBag.ReturneeID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", returnPackage.ReturneeID);
            return View(returnPackage);
        }

        // GET: Client/ReturnPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnPackage returnPackage = db.ReturnPackage.Find(id);
            if (returnPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", returnPackage.ReturnCompanyID);
            ViewBag.ReturneeID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", returnPackage.ReturneeID);
            return View(returnPackage);
        }

        // POST: Client/ReturnPackages/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReturnDataID,ReturnArrivalDate,ReturnCompanyID,Returnee,ReturneeID,Sign,StaffID")] ReturnPackage returnPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnPackage).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReturnCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", returnPackage.ReturnCompanyID);
            ViewBag.ReturneeID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", returnPackage.ReturneeID);
            return View(returnPackage);
        }

        // GET: Client/ReturnPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnPackage returnPackage = db.ReturnPackage.Find(id);
            if (returnPackage == null)
            {
                return HttpNotFound();
            }
            return View(returnPackage);
        }

        // POST: Client/ReturnPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnPackage returnPackage = db.ReturnPackage.Find(id);
            db.ReturnPackage.Remove(returnPackage);
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
