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
    public class ResidentAccountController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/ResidentAccount
        public ActionResult Index()
        {
            return View(db.ResidentASPUsers);
        }

        public ActionResult NoAccountResident()
        {
            return PartialView("_NoAccountResident",db.NoAccountResident);
        }

        public ActionResult NoResidentAccount()
        {
            var q = from a in db.NoBindingResidentAccount
                    where a.RoleId != "admin"
                    select a;
            return PartialView("_NoResidentAccount", q);
        }

        public ActionResult AccountManagement()
        {
            return View();
        }

        // GET: Admin/ResidentAccount/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidentASPUsers residentASPUsers = db.ResidentASPUsers.Find(id);
            if (residentASPUsers == null)
            {
                return HttpNotFound();
            }
            return View(residentASPUsers);
        }

        // POST: Admin/ResidentAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ResidentASPUsers residentASPUsers = db.ResidentASPUsers.Find(id);
            db.ResidentASPUsers.Remove(residentASPUsers);
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
