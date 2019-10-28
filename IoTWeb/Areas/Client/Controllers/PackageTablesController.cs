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
    [Authorize(Roles = "user,admin")]
    public class PackageTablesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/PackageTables
        public ActionResult Index()
        {
            var packageTable = db.PackageTable.Include(p => p.PackageCompany).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p=>p.Sign==false);
            return View(packageTable.ToList());
        }

        // GET: Client/PackageTables/Details/5
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
