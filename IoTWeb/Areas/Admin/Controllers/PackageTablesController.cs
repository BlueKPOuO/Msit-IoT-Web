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
    public class PackageTablesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/PackageTables
        public ActionResult Index()
        {
            var packageTable = db.PackageTable.Include(p => p.PackageCompany).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p => p.Sign==false)/*.Select(p => new {p.PackageID,PackageArrivalDate=$"{p.PackageArrivalDate:"},p.PackageCompanyID,p.Receiver,p.ReceiverID,p.Sign,p.StaffID })*/;
            return View(packageTable.ToList());
        }
        public ActionResult Index2()
        {
            var packageTable = db.PackageTable.Include(p => p.PackageCompany).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p => p.Sign == true)/*.Select(p => new {p.PackageID,PackageArrivalDate=$"{p.PackageArrivalDate:"},p.PackageCompanyID,p.Receiver,p.ReceiverID,p.Sign,p.StaffID })*/;
            return View(packageTable.ToList());
        }

        // GET: Admin/PackageTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTable packageTable = db.PackageTable.Find(id);
            if (packageTable == null)
            {
                return HttpNotFound();
            }
            return View(packageTable);
        }

        // GET: Admin/PackageTables/Create
        public ActionResult Create()
        {
            ViewBag.PackageCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName");
            ViewBag.ReceiverID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName");
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName");
            return View();
        }

        // POST: Admin/PackageTables/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageID,PackageArrivalDate,PackageCompanyID,Receiver,ReceiverID,Sign,StaffID")] PackageTable packageTable)
        {
            
            if (ModelState.IsValid)
            {                
                db.PackageTable.Add(packageTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", packageTable.PackageCompanyID);
            ViewBag.ReceiverID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", packageTable.ReceiverID);
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", packageTable.StaffID);
            return View(packageTable);
        }

        // GET: Admin/PackageTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTable packageTable = db.PackageTable.Find(id);
            if (packageTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", packageTable.PackageCompanyID);
            ViewBag.ReceiverID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", packageTable.ReceiverID);
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", packageTable.StaffID);
            return View(packageTable);
        }

        // POST: Admin/PackageTables/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageID,PackageArrivalDate,PackageCompanyID,Receiver,ReceiverID,Sign,StaffID,SignedDate")] PackageTable packageTable)
        {
            if (ModelState.IsValid)
            {
                var a = db.PackageTable.Where(n => n.PackageID == packageTable.PackageID).Select(n => n.ReceiverID).First();
                packageTable.SignedDate = DateTime.Now;
                packageTable.ReceiverID = a;
                db.PackageTable.Select(n => n.Sign == true);
                db.Entry(packageTable).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageCompanyID = new SelectList(db.PackageCompany, "PackageCompanyID", "CompanyName", packageTable.PackageCompanyID);
            ViewBag.ReceiverID = new SelectList(db.ResidentDataTable, "ResidentID", "ResidentName", packageTable.ReceiverID);
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", packageTable.StaffID);
            return View(packageTable);
        }

        // GET: Admin/PackageTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTable packageTable = db.PackageTable.Find(id);
            if (packageTable == null)
            {
                return HttpNotFound();
            }
            return View(packageTable);
        }

        // POST: Admin/PackageTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageTable packageTable = db.PackageTable.Find(id);
            db.PackageTable.Remove(packageTable);
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
