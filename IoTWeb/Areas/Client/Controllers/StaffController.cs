using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Client.Controllers
{
    public class StaffController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        // GET: Client/Staff
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getStaff()
        {
            var staffList = db.StaffDataTable.Where(s => s.OnWork == true).Select(s => new { s.StaffID,s.StaffName});
            return Json(staffList, JsonRequestBehavior.AllowGet);
        }
    }
}