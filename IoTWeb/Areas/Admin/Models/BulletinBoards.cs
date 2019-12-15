using IoTWeb.Models;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTWeb.Areas.Admin.Models
{
    public class BulletinBoards
    {
        public string SearchTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchName { get; set; }

        public IPagedList<BulletinBoard>  Bulletins { get; set; }

        public int Page { get; set; }  // 頁碼

        
        public BulletinBoards()
        {
            SearchTitle = string.Empty;
            StartDate = null;
            EndDate =null;
            SearchName = string.Empty;
            Page = 0;
        }
    }
}
