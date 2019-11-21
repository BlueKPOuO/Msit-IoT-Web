using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IoTWeb.Areas.Client.Controllers
{
    public class ManagementFeesController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        // GET: Client/ManagementFees
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult GetFee()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();
            var mf = db.ManagementFee.Where(r => r.ResidentID == ResidentId && r.PaidDate == null).Select(f => new
                {
                    f.Year,
                    f.Month,
                    f.Fee
                })
            ;
            return Json(mf, JsonRequestBehavior.AllowGet);
        }
    }
}