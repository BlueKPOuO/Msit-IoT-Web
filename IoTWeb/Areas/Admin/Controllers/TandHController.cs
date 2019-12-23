using IoTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoTWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
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

        [AllowAnonymous]
        public ActionResult TempHis()
        {
            var templist = db.HTDataTable.Select(t => new { t.Time, t.Temperature }).ToList();

            return Json(templist, JsonRequestBehavior.AllowGet);
        }
        
        public void OutJson(List<double> data)
        {
            
        }

        // Admin/TandH/GasDataY
        [AllowAnonymous]
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
                if (id.IndexOf('~') > -1)
                {//範圍時間
                    string[] Time_range = id.Split('~');
                    DateTime fromTime = DateTime.Parse(Time_range[0]);
                    DateTime toTime = DateTime.Parse(Time_range[1]).AddDays(1);

                    var gasSenserDatas = db.GasSenserData.Where(n => n.Time > fromTime && n.Time < toTime);
                    int count = gasSenserDatas.Count();
                    int each = count / 60;
                    int TimesCount = 0;
                    List<double> output = new List<double>();
                    while (TimesCount <= 60)
                    {
                        var NaData = gasSenserDatas.OrderBy(n=>n.Time).Skip(TimesCount * each).Take(each).Select(n => n.Gasvalue).ToList();
                        if (NaData.Count() < each)
                            break;
                        double avg = 0;
                        for(int i = 0; i < NaData.Count; i++)
                        {
                            avg += NaData[i];
                        }
                        avg = Math.Floor(avg / each);
                        output.Add(avg);
                        TimesCount++;
                    }

                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                var gaslist = db.GasSenserData.Select(n => n.Gasvalue);
                return Json(gaslist, JsonRequestBehavior.AllowGet);
            }
        }

        // Admin/TandH/GasDateX
        [AllowAnonymous]
        public ActionResult GasDateX(string id)
        {
            if (id == null)
            {
                DateTime Recent = DateTime.Now.AddHours(-1);
                var Timelist = db.GasSenserData.Where(n => n.Time > Recent).Select(n => n.Time).ToList();
                List<string> strTimeList = DateFormat(Timelist,"hm");
                return Json(strTimeList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (id.IndexOf('~') > -1)
                {//範圍時間
                    string[] Time_range = id.Split('~');
                    DateTime fromTime = DateTime.Parse(Time_range[0]);
                    DateTime toTime = DateTime.Parse(Time_range[1]).AddDays(1);

                    var gasSenserDatas = db.GasSenserData.Where(n => n.Time > fromTime && n.Time < toTime);
                    int count = gasSenserDatas.Count();
                    int each = count / 60;
                    int TimesCount = 0;
                    List<DateTime> dtlist = new List<DateTime>();
                    while (TimesCount <= 60)
                    {
                        var NaData = gasSenserDatas.OrderBy(n => n.Time).Skip(TimesCount * each).Take(each).Select(n => n.Time).ToList();
                        if (NaData.Count()<each)
                            break;
                        double sum = 0;
                        for (int i = 0; i < NaData.Count; i++)
                        {
                            sum += (NaData[i] - new DateTime(1970, 1, 1)).TotalMilliseconds;
                        }
                        sum = Math.Floor(sum / each);
                        DateTime dateTime = (new DateTime(1970, 1, 1)).AddMilliseconds(sum);
                        dtlist.Add(dateTime);
                        TimesCount++;
                    }
                    List<string> output = DateFormat(dtlist, "ymdhm");
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                var Timelist = db.GasSenserData.Select(n => n.Gasvalue);
                return Json(Timelist, JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public List<string> DateFormat(List<DateTime> dates,string type)
        {
            List<string> result = new List<string>();
            switch (type)
            {
                case "hm":
                    foreach (DateTime date in dates)
                    {
                        var element = date.ToString("hh:mm");
                        result.Add(element);
                    }
                    break;
                case "ymd":
                    foreach (DateTime date in dates)
                    {
                        var element = date.ToString("yyyy/MM/dd");
                        result.Add(element);
                    }
                    break;
                case "ymdhm":
                    foreach (DateTime date in dates)
                    {
                        var element = date.ToString("yyyy/MM/dd hh:mm");
                        result.Add(element);
                    }
                    break;
            }
            
            return result;
        }

        public class ForDate
        {
            public int? Year { get; set; }
            public int? Month { get; set; }
            public int? day { get; set; }
            public string date
            {
                get
                {
                    string strMonth = Month < 10 ? "0" + Month : Month.ToString();
                    if(day == null)
                    {
                        return this.Year + "-" + strMonth;
                    }
                    else
                    {
                        string strday = day < 10 ? "0" + day : day.ToString();
                        return this.Year + "-" + strMonth + "-" + strday;
                    }
                }
            }
        }

        // Admin/TandH/GetTime
        [AllowAnonymous]
        public ActionResult GetTime(string id)
        {
            if (id == null)
            {
                var datelist1 = from a in db.GasSenserData.AsEnumerable()
                                select new ForDate { Year = a.Time.Year, Month = a.Time.Month };
                var datelist2 = datelist1.Select(n => n.date).Distinct();
                return Json(datelist2, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (id.IndexOf('-') > -1)
                {
                    if (int.TryParse(id.Substring(0, 4), out int year1) && int.TryParse(id.Substring(5), out int month1))
                    {
                        var daylist = from a in db.GasSenserData.AsEnumerable()
                                      where a.Time.Year == year1 && a.Time.Month == month1
                                      select a.Time.Day < 10 ? "0" + a.Time.Day : a.Time.Day.ToString();
                        var daylist2 = daylist.Distinct();
                        return Json(daylist2, JsonRequestBehavior.AllowGet);
                    }
                    else if (int.TryParse(id.Substring(0, 4), out int year2) && int.TryParse(id.Substring(5, 2), out int month2) && int.TryParse(id.Substring(8, 2), out int day2))
                    {
                        var list = from a in db.GasSenserData.AsEnumerable()
                                   where a.Time.Year >= year2 && a.Time.Month >= month2
                                   select new ForDate { Year = a.Time.Year, Month = a.Time.Month };
                        var list2 = list.Distinct();
                        return Json(list2, JsonRequestBehavior.AllowGet);
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else if (id.IndexOf("range") > -1) 
                {
                    DateTime startTime = db.GasSenserData.First().Time;
                    int count = db.GasSenserData.Count() - 1;
                    DateTime endTime = db.GasSenserData.AsEnumerable().Skip(count).First().Time;
                    List<DateTime> Timelist_range = new List<DateTime> { startTime, endTime };
                    List<string> strTimeList = DateFormat(Timelist_range,"ymd");
                    return Json(strTimeList, JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}