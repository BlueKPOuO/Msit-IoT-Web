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
        private IndependentEntities idb = new IndependentEntities();

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
        [HttpPost]
        public ActionResult AccountManagement([Bind(Include = "Name,ASPAccount")] ASPAccountApply Apply)
        {
            //var nameList = db.
            bool IsNameOk = db.NoAccountResident.Any(a => a.ResidentName == Apply.Name);
            bool IsASPAccOk = db.NoBindingResidentAccount.Where(a => a.RoleId != "admin").Any(a => a.UserName == Apply.ASPAccount);
            if (IsNameOk && IsASPAccOk)
            {
                string AccountId = db.AspNetUsers.Where(n => n.UserName == Apply.ASPAccount).Select(n => n.Id).First();
                int ResidId = db.ResidentDataTable.Where(n => n.ResidentName == Apply.Name).Select(n => n.ResidentID).First();
                AspNetUserRoles anur = new AspNetUserRoles { UserId=AccountId, RoleId="user" };
                AspUserResidentData aurd = new AspUserResidentData { AspUserId = AccountId, ResidentID = ResidId};
                idb.AspUserResidentData.Add(aurd);
                db.AspNetUserRoles.Add(anur);
                idb.SaveChanges();
                db.SaveChanges();

                return RedirectToAction("Index", "ResidentAccount", new { Area = "Admin" });
            }
            else
            {
                //ViewBag.Alert = $"$('alert').html='住戶名或帳號不存在';";
                ViewBag.Alert = true;
                return View();
            }
            
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
