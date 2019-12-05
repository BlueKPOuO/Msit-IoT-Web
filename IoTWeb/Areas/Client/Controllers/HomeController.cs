using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        [Authorize(Roles = "user,admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public JsonResult Calendar()
        //{
        //    //var EquipRes = db.
        //    //return Json();
        //}
    }
}