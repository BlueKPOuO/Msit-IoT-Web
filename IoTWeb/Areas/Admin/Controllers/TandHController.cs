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
        
        public void OutJson(List<double> data)
        {
            
        }

        public ActionResult GasDataY(string id)
        {
            if (id == null)
            {

                var gaslist = db.GasSenserData.AsEnumerable().Where(n=>n.Time.Month>DateTime.Now.Month).Select(n => n.Gasvalue).ToList();

                return Json(gaslist, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var gaslist = db.GasSenserData.Select(n => n.Gasvalue );
                return Json(gaslist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GasDataX(string id)
        {
            if (id == null)
            {

                var Timelist = db.GasSenserData.Select(n => n.Gasvalue).ToList();

                return Json(Timelist, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var Timelist = db.GasSenserData.Select(n => n.Gasvalue);
                return Json(Timelist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}