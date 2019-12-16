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
    public class ResidentDataTablesController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/ResidentDataTables
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult history()
        {
            var history = db.ResidentDataTable.Where(r => r.Living == false);
            return View(history);
        }

        public ActionResult GetData()
        {
            var resList = db.ResidentDataTable.Where(r => r.Living).Select(r => new { r.ResidentID, r.ResidentName, r.ResidentPhone, r.ResidentIDNumber, r.CommunityAddress }).ToList();
            return Json(resList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ResidentDataTable());
            else
            {
                //var resident2 = from r in db.ResidentDataTable
                //                where r.ResidentID == id
                //                select new ResidentViewModel
                //                {
                //                    ResidentID = r.ResidentID,
                //                    ResidentName = r.ResidentName,
                //                    ResidentPhone = r.ResidentPhone,
                //                    ResidentIDNumber = r.ResidentIDNumber,
                //                    CommunityAddress = r.CommunityAddress,
                //                    Living = r.Living,
                //                    img = r.img
                //                };
                return View(db.ResidentDataTable.Where(x => x.ResidentID == id).FirstOrDefault<ResidentDataTable>());
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(ResidentDataTable res)
        {
            if (res.ResidentID == 0)
            {
                db.ResidentDataTable.Add(res);
                db.SaveChanges();
                return Json(new { success = true, message = "新增住戶完成！" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Entry(res).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "修改完成！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ResidentDataTable res = db.ResidentDataTable.Where(x => x.ResidentID == id).FirstOrDefault<ResidentDataTable>();
            db.ResidentDataTable.Remove(res);
            db.SaveChanges();
            return Json(new { success = true, message = "刪除完成！" }, JsonRequestBehavior.AllowGet);
        }
    }
}
