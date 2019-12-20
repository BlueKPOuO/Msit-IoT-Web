using IoTWeb.Areas.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IoTWeb.Controllers
{
    [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Registered-Error")]
    [Authorize(Roles = "user,admin")]
    public class HomeController : Controller
    {
        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "Registered-Error")]
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

        public JsonResult GetIoTData()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SignalrConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(@"select [Topic],[Value] from [dbo].[ImmediateIoTData]", connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_Onchange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var listData = reader.Cast<IDataRecord>().Select(x => new
                    {
                        Topic = (string)x["Topic"],
                        Value = (string)x["Value"],
                    }).ToList();
                    //var listdata2 = listData.Select(n => new { Topic = n.Topic.ToString(), Value1 = n.Value1.ToString(), Value2 = n.Value2.ToString() }).ToList();
                    return Json(new { listData = listData}, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetAlert()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SignalrConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(@"select [Place],[Alert],[PS] where[Alert]==true from [dbo].[IoTAlert]", connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_Onchange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var listData = reader.Cast<IDataRecord>().Select(x => new
                    {
                        Place = (string)x["Place"],
                        Alert = (string)x["Alert"],
                        Type = (string)x["PS"]
                    }).ToList();
                    //var listdata2 = listData.Select(n => new { Topic = n.Topic.ToString(), Value1 = n.Value1.ToString(), Value2 = n.Value2.ToString() }).ToList();
                    return Json(new { listData = listData }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private void dependency_Onchange(object sender, SqlNotificationEventArgs e)
        {
            IoTDataHub.Show();
        }
    }
}