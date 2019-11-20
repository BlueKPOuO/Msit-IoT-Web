﻿using System.Linq;
using System.Web.Mvc;
using IoTWeb.Models;
using Microsoft.AspNet.Identity;


namespace IoTWeb.Areas.Client.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class BulletinBoardsController: Controller
        {
            private Buliding_ManagementEntities db = new Buliding_ManagementEntities();

       // GET: BulletinBoards
            public ActionResult Index()
        {
            string NowUser = User.Identity.GetUserName();
            int ResidentId = db.ResidentASPUsers.Where(n => n.UserName == NowUser).Select(n => n.ResidentID).First();

            var list = db.BulletinBoard.ToList();
            return View(list);
        }
        public ActionResult Read(ReadRequest request)
            {
                BulletinBoard target = db.BulletinBoard.ToList().Find(x => x.annID == request.annID);
            TempData["message"] = target;
            return RedirectToAction("Index", "BulletinBoards");

            //return View(target);
            }
           
            public ActionResult light()
            {
                return View();
            }
            public class ReadRequest
            {
                public int annID { get; set; }
            }

          
        }
    }

