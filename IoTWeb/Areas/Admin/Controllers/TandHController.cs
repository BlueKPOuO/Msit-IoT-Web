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

        public ActionResult GasChart()
        {
            return View();
        }

        public ActionResult TempHis()
        {
            var templist = db.HTDataTable.Select(t => new { t.Time, t.Temperature }).ToList();

            return Json(templist, JsonRequestBehavior.AllowGet);
        }
        
        public void OutJson(List<double> data)
        {
            
        }

        public ActionResult GasDataY(string id)
        {
            if (id == null)
            {
                DateTime Recent = DateTime.Now.AddHours(-1);
                var gaslist = db.GasSenserData.AsEnumerable().Where(n => n.Time > Recent).Select(n => n.Gasvalue).ToList();
                return Json(gaslist, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var gaslist = db.GasSenserData.Select(n => n.Gasvalue );
                return Json(gaslist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GasDateX(string id)
        {
            if (id == null)
            {
                DateTime Recent = DateTime.Now.AddHours(-1);
                var Timelist = db.GasSenserData.Where(n => n.Time > Recent).Select(n => n.Time).ToList();
                List<string> strTimeList = DateFormat(Timelist);
                return Json(strTimeList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Timelist = db.GasSenserData.Select(n => n.Gasvalue);
                return Json(Timelist, JsonRequestBehavior.AllowGet);
            }
        }

        public List<string> DateFormat(List<DateTime> dates)
        {
            List<string> result = new List<string>();
            foreach(DateTime date in dates)
            {
                var element = date.ToString("hh:mm");
                result.Add(element);
            }
            return result;
        }

        public ActionResult GetTime(string id)
        {

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}