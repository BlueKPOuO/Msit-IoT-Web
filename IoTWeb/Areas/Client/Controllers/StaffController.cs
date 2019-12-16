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
            var staffList = db.StaffDataTable.AsEnumerable().Where(s => s.OnWork == true).Select(s => new { s.StaffID,s.StaffName, EntryDate = s.EntryDate.ToShortDateString(),s.img}).ToList();
            return Json(staffList, JsonRequestBehavior.AllowGet);
        }
    }
}