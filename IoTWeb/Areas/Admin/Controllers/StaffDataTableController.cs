using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IoTWeb.Models;
using System.IO;

namespace IoTWeb.Areas.Admin.Controllers
{
    public class StaffDataTableController : Controller
    {
        private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

        // GET: Admin/StaffDataTable
        public async Task<ActionResult> Index()
        {
            var staffDataTable = db.StaffDataTable.Include(s => s.ShiftTable).Where(s => s.OnWork == true);
            return View(await staffDataTable.ToListAsync());
        }

        // GET: Admin/StaffDataTable/Index2
        public async Task<ActionResult> Index2()
        {
            var staffDataTable = db.StaffDataTable.Include(s => s.ShiftTable);
            return View(await staffDataTable.ToListAsync());
        }

        //秀圖片Action
        public FileResult ShowImg(string id)
        {
            byte[] content = db.StaffDataTable.Find(id).img;
            return File(content, "image/jpeg");
        }

        //將資料塞入資料表欄位
        private void GetImage(StaffDataTable staffDataTable, dynamic file)
        {
            byte[] data = null;
            using (BinaryReader br = new BinaryReader(file.InputStream))
            {
                data = br.ReadBytes(file.ContentLength);
            }
            staffDataTable.img = data;
        }

        // GET: Admin/StaffDataTable/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDataTable staffDataTable = await db.StaffDataTable.FindAsync(id);
            if (staffDataTable == null)
            {
                return HttpNotFound();
            }
            return View(staffDataTable);
        }

        // GET: Admin/StaffDataTable/Create
        public ActionResult Create()
        {
            ViewBag.ShiftID = new SelectList(db.ShiftTable, "ShiftID", "ShiftID");
            return View();
        }

        // POST: Admin/StaffDataTable/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StaffID,StaffName,EntryDate,LeaveDate,StaffPhone,StaffLINE_ID,OnWork,ShiftID,img")] StaffDataTable staffDataTable)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)//如果File1有資料傳入(上傳圖片)
                {
                    GetImage(staffDataTable, Request.Files["File1"]);
                }

                db.StaffDataTable.Add(staffDataTable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ShiftID = new SelectList(db.ShiftTable, "ShiftID", "ShiftID", staffDataTable.ShiftID);
            return View(staffDataTable);
        }

        // GET: Admin/StaffDataTable/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDataTable staffDataTable = await db.StaffDataTable.FindAsync(id);
            if (staffDataTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftID = new SelectList(db.ShiftTable, "ShiftID", "ShiftID", staffDataTable.ShiftID);
            return View(staffDataTable);
        }

        // POST: Admin/StaffDataTable/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StaffID,StaffName,EntryDate,LeaveDate,StaffPhone,StaffLINE_ID,OnWork,ShiftID,img")] StaffDataTable staffDataTable)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files["File1"].ContentLength != 0)
                {
                    GetImage(staffDataTable, Request.Files["File1"]);
                }
                else
                {
                    StaffDataTable s = db.StaffDataTable.Find(staffDataTable.StaffID);
                    s.StaffName = staffDataTable.StaffName;
                    s.EntryDate = staffDataTable.EntryDate;
                    s.LeaveDate = staffDataTable.LeaveDate;
                    s.StaffPhone = staffDataTable.StaffPhone;
                    s.StaffLINE_ID = staffDataTable.StaffLINE_ID;
                    s.OnWork = staffDataTable.OnWork;
                    s.ShiftID = staffDataTable.ShiftID;
                    staffDataTable = s;
                }
                db.Entry(staffDataTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftID = new SelectList(db.ShiftTable, "ShiftID", "ShiftID", staffDataTable.ShiftID);
            return View(staffDataTable);
        }

        // GET: Admin/StaffDataTable/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffDataTable staffDataTable = await db.StaffDataTable.FindAsync(id);
            if (staffDataTable == null)
            {
                return HttpNotFound();
            }
            return View(staffDataTable);
        }

        // POST: Admin/StaffDataTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StaffDataTable staffDataTable = await db.StaffDataTable.FindAsync(id);
            db.StaffDataTable.Remove(staffDataTable);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
