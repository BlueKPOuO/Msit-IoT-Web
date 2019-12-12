using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Admin.Controllers
{
    public class TandHController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        // GET: Admin/TandH
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TempHis()
        {
            var templist = db.HTDataTable.Select(t => new { t.Time, t.Temperature }).ToList();

            return Json(templist, JsonRequestBehavior.AllowGet);
        }
    }
}