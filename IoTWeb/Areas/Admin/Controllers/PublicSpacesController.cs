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
    public class PublicSpacesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: PublicSpaces
        public ActionResult Index()
        {
            var publicSpace = db.PublicSpace.Include(p => p.Location).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p => p.借用審核 == false);
            return View(publicSpace);
        }
        public ActionResult Index2()
        {
            var publicSpace = db.PublicSpace.Include(p => p.Location).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p => p.History == true);
            return View(publicSpace);
        }

        // GET: PublicSpaces/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublicSpace publicSpace = db.PublicSpace.Find(id);
            if (publicSpace == null)
            {
                return HttpNotFound();
            }
            return View(publicSpace);
        }

        // GET: PublicSpaces/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Location1");
            return View();
        }

        // POST: PublicSpaces/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResidentID,StaffID,seq,barrierName,LocationID,StartTime,EndTime,Reason")] PublicSpace publicSpace)
        {
            if (ModelState.IsValid)
            {
                db.PublicSpace.Add(publicSpace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Location1", publicSpace.LocationID);
            return View(publicSpace);
        }

        // GET: PublicSpaces/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublicSpace publicSpace = db.PublicSpace.Find(id);
            if (publicSpace == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Location1", publicSpace.LocationID);
            return View(publicSpace);
        }

        // POST: PublicSpaces/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResidentID,StaffID,seq,barrierName,LocationID,StartTime,EndTime,Reason,DateTimeNow,借用審核,History")] PublicSpace publicSpace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicSpace).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw;
                }
                

                return RedirectToAction("Index");
            }            
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Location1", publicSpace.LocationID);
            return View(publicSpace);
        }

        // GET: PublicSpaces/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublicSpace publicSpace = db.PublicSpace.Find(id);
            if (publicSpace == null)
            {
                return HttpNotFound();
            }
            return View(publicSpace);
        }

        // POST: PublicSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PublicSpace publicSpace = db.PublicSpace.Find(id);
            db.PublicSpace.Remove(publicSpace);
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
