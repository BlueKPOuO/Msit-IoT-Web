using IoTWeb.Areas.Admin.Models;
using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace IoTWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class BulletinBoardsController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();
        private const int PageSize = 7;
        // GET: Admin/BulletinBoards
        public ActionResult Index(int page= 0)
        {
            BulletinBoards viewmodel = new BulletinBoards();
            viewmodel.Bulletins= db.BulletinBoard.Include(b => b.StaffDataTable).OrderBy(p=>p.annID).ToPagedList(0, PageSize);
          
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Index(BulletinBoards request)
        {
            
            IQueryable<BulletinBoard> Bulletins = db.BulletinBoard.Include(b => b.StaffDataTable);

            // 如果有輸入標題名稱作為搜尋條件時
            if (!string.IsNullOrWhiteSpace(request.SearchTitle))
            { Bulletins = Bulletins.Where(p => p.annTitle.Contains(request.SearchTitle)); }

            if (request.StartDate.HasValue&&request.EndDate.HasValue)
            { Bulletins = Bulletins.Where(p => p.annDate>=request.StartDate.Value&&p.annDate<=request.EndDate.Value); }

            // 如果有輸入生產工廠作為搜尋條件時
            if (!string.IsNullOrWhiteSpace(request.SearchName))
            { Bulletins = Bulletins.Where(p => p.StaffDataTable.StaffName.Contains(request.SearchName)); }



            // 回傳搜尋結果
            request.Bulletins = Bulletins.OrderBy(p => p.annID).ToPagedList(request.Page > 0 ? request.Page - 1 : 0, PageSize);

            return View(request);
        }

        public ActionResult Index(string search, DateTime? startdate, DateTime? enddate)
        {
            ViewBag.Datetime = DateTime.UtcNow;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            IQueryable<BulletinBoards> bbs = db.BulletinBoards;

            if (search != null)
            {
                bbs = bbs.Where(x => x.StartDate.HasValue.Equals(search) || x.EndDate.HasValue.Equals(search));
            }
            if (startdate.HasValue)
            {
                bbs = bbs.Where(x => x.StartDate > startdate.Value);
            }
            if (enddate.HasValue)
            {
                bbs = bbs.Where(x => x.EndDate < enddate.Value);
            }
            // At this point the query has generated a SQL statement based on the conditions above,
            // but it will not be executed until the until the next line - i.e. when calling .ToList()
            return View(bbs.ToList());
        }
        //檔案上傳
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "File"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return View();
        }
        //檔案下載
        public ActionResult Download(int annID)
        {
            var fileA = db.BulletinBoard.Find(annID);
            Stream fileStream = new MemoryStream(fileA.annAnnex);
            return File(fileStream, "application/octet-stream", fileA.annFilename);
        }

        // GET: Admin/BulletinBoards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            if (bulletinBoard == null)
            {
                return HttpNotFound();
            }
            return View(bulletinBoard);
        }

        // GET: Admin/BulletinBoards/Create
        public ActionResult Create()
        {

            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName");
            ViewBag.annGrade1 = new SelectList(db.BulletinBoard, "annGrade");
            var GradeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="無", Value="無" },
                new SelectListItem {Text="公告", Value="公告" },
                new SelectListItem {Text="重要公告", Value="重要公告" },
            };
            ViewBag.GradeList = GradeList;

            var ClassList = new List<SelectListItem>()
            {
                new SelectListItem {Text="一般公告", Value="一般公告" },
                new SelectListItem {Text="人事公告", Value="人事公告" },
                new SelectListItem {Text="停車公告", Value="停車公告" },
                new SelectListItem {Text="施工公告", Value="施工公告" },
                new SelectListItem {Text="會議通知", Value="會議通知" },
                new SelectListItem {Text="設備更換", Value="設備更換" },
            };
            ViewBag.ClassList = ClassList;
            return View();

        }

        // POST: Admin/BulletinBoards/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)
                {
                    bulletinBoard.annAnnex= Getbyte( Request.Files["File1"]);
                    bulletinBoard.annFilename = Request.Files["File1"].FileName;
                }
                db.BulletinBoard.Add(bulletinBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
            ViewBag.annGrade1 = new SelectList(db.BulletinBoard, "annGrade", bulletinBoard.annGrade);

            return View(bulletinBoard);
        }

        private byte[] Getbyte(dynamic file)
        {
            byte[] data = null;
            using (BinaryReader br = new BinaryReader(file.InputStream))
            {
                data = br.ReadBytes(file.ContentLength);
            }
            return data;
        }

        // GET: Admin/BulletinBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            if (bulletinBoard == null)
            {
                return HttpNotFound();
            }
            //NewMethod(bulletinBoard);
            var GradeList = new List<SelectListItem>()
            {
                new SelectListItem {Text="無", Value="無" },
                new SelectListItem {Text="公告", Value="公告" },
                new SelectListItem {Text="重要公告", Value="重要公告" },
            };
            GradeList.Where(q => q.Value == bulletinBoard.annGrade).First().Selected = true;
            ViewBag.GradeList = GradeList;

            var ClassList = new List<SelectListItem>()
            {
                new SelectListItem {Text="一般公告", Value="一般公告" },
                new SelectListItem {Text="人事公告", Value="人事公告" },
                new SelectListItem {Text="停車公告", Value="停車公告" },
                new SelectListItem {Text="施工公告", Value="施工公告" },
                new SelectListItem {Text="會議通知", Value="會議通知" },
                new SelectListItem {Text="設備更換", Value="設備更換" },
            };
            ClassList.Where(q => q.Value == bulletinBoard.annClass).First().Selected = true;
            ViewBag.ClassList = ClassList;


            return View(bulletinBoard);
        }

        private void NewMethod(BulletinBoard bulletinBoard)
        {
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
        }

        // POST: Admin/BulletinBoards/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)
                {
                    bulletinBoard.annAnnex = Getbyte(Request.Files["File1"]);
                    bulletinBoard.annFilename = Request.Files["File1"].FileName;
                }
                db.Entry(bulletinBoard).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.StaffDataTable, "StaffID", "StaffName", bulletinBoard.StaffID);
            return View(bulletinBoard);
        }
        public ActionResult Delete(int id)
        {
            var todo = db.BulletinBoard.Where(m => m.annID == id).FirstOrDefault();
            db.BulletinBoard.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //// GET: Admin/BulletinBoards/Delete/5

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
        //    if (bulletinBoard == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bulletinBoard);
        //}

        //// POST: Admin/BulletinBoards/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
        //    db.BulletinBoard.Remove(bulletinBoard);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
