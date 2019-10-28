using System.Linq;
using System.Web.Mvc;
using IoTWeb.Models;

namespace IoTWeb.Controllers
{
     public class BulletinBoardsController: Controller
        {
            private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

            // GET: BulletinBoards
            public ActionResult Index()
            {
                var list = db.BulletinBoard.ToList();
                for (int i = 1; i <= list.Count; i++)
                {
                    list[i - 1].No = i;
                }
                return View(list);
            }
            public ActionResult Read(ReadRequest request)
            {
                BulletinBoard target = db.BulletinBoard.ToList().Find(x => x.annID == request.annID);
                return View(target);
            }
            public ActionResult PublicSpace()
            {
                return View();
            }
            public ActionResult temperature()
            {
                return View();
            }
            public ActionResult light()
            {
                return View();
            }
            public class ReadRequest
            {
                public int annID { get; set; }
            }

            //// GET: BulletinBoards/Details/5
            //public ActionResult Details(int? id)
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

            //// GET: BulletinBoards/Create
            //public ActionResult Create()
            //{
            //    return View();
            //}

            //// POST: BulletinBoards/Create
            //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
            //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Create([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.BulletinBoard.Add(bulletinBoard);
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    return View(bulletinBoard);
            //}

            //// GET: BulletinBoards/Edit/5
            //public ActionResult Edit(int? id)
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

            //// POST: BulletinBoards/Edit/5
            //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
            //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Edit([Bind(Include = "StaffID,annID,annGrade,annClass,annDate,annTitle,annContent,annAnnex,annFilename")] BulletinBoard bulletinBoard)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(bulletinBoard).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    return View(bulletinBoard);
            //}

            //// GET: BulletinBoards/Delete/5
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

            //// POST: BulletinBoards/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public ActionResult DeleteConfirmed(int id)
            //{
            //    BulletinBoard bulletinBoard = db.BulletinBoard.Find(id);
            //    db.BulletinBoard.Remove(bulletinBoard);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //protected override void Dispose(bool disposing)
            //{
            //    if (disposing)
            //    {
            //        db.Dispose();
            //    }
            //    base.Dispose(disposing);
            //}
        }
    }

