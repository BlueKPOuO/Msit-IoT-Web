using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace IoTWeb.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        private IndependentEntities idb = new IndependentEntities();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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
                //ViewBag.Alert = true;
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

        public ActionResult StaffAccountCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StaffAccountCreate(StaffRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //查住戶驗證碼是否存在
                bool IsIDNOk = db.StaffDataTable.Any(n => n.StaffID == model.StaffID);
                string StaffID = "";
                ApplicationUser user;
                IdentityResult result = new IdentityResult();
                if (IsIDNOk)
                {   //抓取住戶編號
                    StaffID = model.StaffID;

                    bool IsNameOk = db.StaffASPUsers.Any(n=>n.StaffID==StaffID);//確認該住戶是否已有帳號
                    if (!IsNameOk)
                    {
                        user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                        result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            string Account = model.Username;//抓登入者帳號
                            string AccountID = db.AspNetUsers.Where(n => n.UserName == Account).Select(n => n.Id).First();

                            //用帳號抓帳號編號
                            AspNetUserRoles anur = new AspNetUserRoles { UserId = AccountID, RoleId = "admin" };//設帳號權限
                            StaffAspUserData saud = new StaffAspUserData { AspUserId = AccountID, StaffID = StaffID };
                            idb.StaffAspUserData.Add(saud);
                            db.AspNetUserRoles.Add(anur);
                            idb.SaveChanges();
                            db.SaveChanges();

                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                            // 如需如何進行帳戶確認及密碼重設的詳細資訊，請前往 https://go.microsoft.com/fwlink/?LinkID=320771
                            // 傳送包含此連結的電子郵件
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { Area="", userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                            await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");

                        }
                    }
                    else
                    {
                        ViewBag.errorMessage = "該警衛已經註冊過帳號";
                    }
                }
                else
                {
                    ViewBag.errorMessage = "警衛編號有問題";
                }

                AddErrors(result);
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region help
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
                ViewBag.errorMessage += error;
            }
        }

        #endregion
    }
}
