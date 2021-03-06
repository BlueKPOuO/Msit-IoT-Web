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
    [Authorize(Roles = "user,admin")]
    public class PackageTablesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Client/PackageTables
        public ActionResult Index()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();

            var packageTable = db.PackageTable.Include(p => p.PackageCompany).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p=>p.Sign==false&& p.ReceiverID == ResidentId);
            return View(packageTable.ToList());
        }
        public ActionResult Index2()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var packageTable = db.PackageTable.Include(p => p.PackageCompany).Include(p => p.ResidentDataTable).Include(p => p.StaffDataTable).Where(p => p.Sign == true && p.ReceiverID == ResidentId);
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
